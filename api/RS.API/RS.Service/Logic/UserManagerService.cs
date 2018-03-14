﻿using RS.Service.Interfaces;
using RS.Common.Enums;
using RS.Common.Extensions;
using RS.Entity;
using RS.ViewModel.User;
using System;
using System.Linq;
using RS.Data.Interfaces;
using RS.Common.CommonData;
using RS.Entity.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace RS.Service.Logic
{
    public class UserManagerService : IUserManagerService
    {
        #region Global Variables
        private readonly ClaimsPrincipal _principal;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IApprovalRepository _approvalRepository;

        #endregion
        public UserManagerService(IPrincipal principal, IUserRepository userRepository, IRoleRepository roleRepository,
            IApprovalRepository approvalRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._approvalRepository = approvalRepository;
            this._principal = principal as ClaimsPrincipal;
        }

        public IResult ChangePassword(string oldPassword, string newPassword)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var authorizedUser = GenericHelper.GetUserClaimDetails((ClaimsIdentity)_principal.Identity);
                var user = _userRepository.GetUser(authorizedUser.UserId, oldPassword);
                if (user.UserId != null)
                {
                    user.Password = newPassword;
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    result.Body = user.UserId;
                }
                else
                {
                    result.Body = null;
                    result.Status = Status.Fail;
                    result.Message = CommonErrorMessages.UserNotFound;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        public IResult CreateUser(UserViewModel user)
        {
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                var duplicateUserName = _userRepository.GetFirstOrDefault(x => (x.IsActive && !x.IsDeleted) && (x.UserName == user.UserName));
                var duplicateEmail = _userRepository.GetFirstOrDefault(x => (x.IsActive && !x.IsDeleted) && (x.Email == user.Email));
                if (duplicateUserName != null)
                {
                    result.Status = Status.Fail;
                    result.Message = UserStatusNotification.DuplicateUserName;
                    result.Body = null;
                    return result;
                }
                else if (duplicateEmail != null)
                {
                    result.Status = Status.Fail;
                    result.Message = UserStatusNotification.DuplicateEmail;
                    result.Body = null;
                    return result;
                }
                else
                {
                    var userModel = new Users();
                    userModel.MapFromViewModel(user, (ClaimsIdentity)_principal.Identity);

                    UserRoles userRole = new UserRoles();
                    userRole.RoleId = user.RoleId;
                    userRole.Role = _roleRepository.GetByID(user.RoleId);
                    userRole.MapAuditColumns((ClaimsIdentity)_principal.Identity);
                    _userRepository.CreateUser(userModel, userRole);
                    result.Body = userModel;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;

        }

        public IResult ForgotPassword(string userName)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var user = _userRepository.GetEmailIdByUserName(userName);
                if (user.UserId != null)
                {
                    result.Body = user.Email;
                }
                else
                {
                    result.Body = null;
                    result.Status = Status.Fail;
                    result.Message = CommonErrorMessages.UserNotFound;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        public IResult GetAllUser()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var allUsers = _userRepository.GetAll().ToList();
                result.Body = allUsers.Select(user =>
                {
                    var getUserRole = user.UserRoles.FirstOrDefault(x => (x.IsActive && !x.IsDeleted));
                    var userViewModel = new UserViewModel();
                    if (getUserRole != null)
                    {
                        userViewModel.RoleId = getUserRole.RoleId;
                        userViewModel.Role = getUserRole.Role.Name;
                    }
                    return userViewModel.MapFromModel(user);
                }).ToList();
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        public IResult GetUserById(Guid id)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var getUser = _userRepository.GetByID(id);
                var getUserRole = getUser.UserRoles.FirstOrDefault(x => (x.IsActive && !x.IsDeleted));
                var user = new UserViewModel();
                if (getUserRole != null)
                {
                    user.RoleId = getUserRole.RoleId;
                    user.Role = getUserRole.Role.Name;
                }
                result.Body = user.MapFromModel(getUser);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        public IResult GetUserDetail()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var identity = (ClaimsIdentity)_principal.Identity;
                result.Body = GenericHelper.GetUserClaimDetails(identity);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        public IResult GetUsersByRole(int roleId)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var users = _userRepository.GetUsersByRole(roleId).ToList();
                result.Body = users.Select(user =>
                {
                    var getUserRole = user.UserRoles.FirstOrDefault(x => (x.IsActive && !x.IsDeleted));
                    var userViewModel = new UserViewModel();
                    userViewModel.FullName = user.FirstName + " " + user.LastName;
                    if (getUserRole != null)
                    {
                        userViewModel.RoleId = getUserRole.RoleId;
                        userViewModel.Role = getUserRole.Role.Name;
                    }
                    return userViewModel.MapFromModel(user);
                }).ToList();
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }
        #region private members

        #endregion private members

        #region public methods

        public IResult LoginUser(string username, string password)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var user = _userRepository.LoginUser(username, password);
                if (user != null)
                {
                    var userView = new UserViewModel();
                    userView.MapFromModel(user, "UserName;");
                    userView.FullName = user.FirstName + " " + user.LastName;
                    var firstOrDefault = user.UserRoles.FirstOrDefault();
                    if (firstOrDefault != null) userView.Role = firstOrDefault.Role.Name;
                    userView.approvalDetail = _approvalRepository.GetApprovalEventsOfUser(user.UserId);
                    result.Body = userView;
                }
                else
                {
                    result.Message = UserStatusNotification.InValidUser;
                    result.Status = Status.Fail;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
                result.Status = Status.Error;

            }
            return result;
        }



        public IResult UpdateUser(UserViewModel user)
        {
            var result = new Result
            {
                Operation = Operation.Update,
                Status = Status.Success
            };
            try
            {
                var duplicateUserName = _userRepository.GetFirstOrDefault(x => (x.IsActive && !x.IsDeleted) && (x.UserName == user.UserName) && (x.UserId != user.UserId));
                if (duplicateUserName != null)
                {
                    result.Status = Status.Fail;
                    result.Message = UserStatusNotification.DuplicateUserName;
                    result.Body = null;
                    return result;
                }

                var duplicateEmail = _userRepository.GetFirstOrDefault(x => (x.IsActive && !x.IsDeleted) && (x.Email == user.Email) && (x.UserId != user.UserId));
                if (duplicateEmail != null)
                {
                    result.Status = Status.Fail;
                    result.Message = UserStatusNotification.DuplicateEmail;
                    result.Body = null;
                    return result;
                }

                var userModel = new Users();
                userModel.MapFromViewModel(user, (ClaimsIdentity)_principal.Identity);
                var userDetail = _userRepository.GetByID(user.UserId);
                var userRoleModel = userDetail.UserRoles.Where(x => (x.IsActive && !x.IsDeleted) && x.RoleId != user.RoleId).ToList();

                if (userRoleModel != null)
                {
                    userRoleModel.ForEach(x => x.MapDeleteColumns((ClaimsIdentity)_principal.Identity));
                    var userRole = new UserRoles();
                    userRole.RoleId = user.RoleId;
                    userRole.Role = _roleRepository.GetByID(user.RoleId);
                    userRole.MapAuditColumns((ClaimsIdentity)_principal.Identity);
                    _userRepository.UpdateUserRole(userRole);
                }
                _userRepository.Update(userModel);
                _userRepository.SaveChanges();
                result.Body = userModel;


            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        public IResult UpdateUserProfile(UserViewModel user)
        {
            var result = new Result
            {
                Operation = Operation.Update,
                Status = Status.Success
            };
            try
            {
                var userId = GenericHelper.GetUserClaimDetails((ClaimsIdentity)_principal.Identity).UserId;
                var duplicateUserName = _userRepository.GetFirstOrDefault(x => (x.IsActive && !x.IsDeleted) && (x.UserName == user.UserName) && (x.UserId != userId));
                if (duplicateUserName != null)
                {
                    result.Status = Status.Fail;
                    result.Message = UserStatusNotification.DuplicateUserName;
                    result.Body = null;
                    return result;
                }

                var duplicateEmail = _userRepository.GetFirstOrDefault(x => (x.IsActive && !x.IsDeleted) && (x.Email == user.Email) && (x.UserId != userId));
                if (duplicateEmail != null)
                {
                    result.Status = Status.Fail;
                    result.Message = UserStatusNotification.DuplicateEmail;
                    result.Body = null;
                    return result;
                }


                var userModel = _userRepository.GetByID(userId);
                userModel.UserId = userId;
                userModel.MapFromViewModel((ClaimsIdentity)_principal.Identity);
                userModel.FirstName = user.FirstName;
                userModel.LastName = user.LastName;
                userModel.Email = user.Email;
                userModel.UserName = user.UserName;
                _userRepository.Update(userModel);
                _userRepository.SaveChanges();
                result.Body = userModel;


            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Error;
            }
            return result;
        }

        #endregion

    }
}
