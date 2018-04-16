﻿using System;
using System.Collections.Generic;
using System.Text;
using RS.Common.CommonData;
using RS.ViewModel.Approval;
using RS.ViewModel.User;

namespace RS.Service.Interfaces
{
    public interface IApprovalManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        IResult GetApprovalEvents(int approvalId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IResult GetAllApprovals();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IResult GetAllApprovalEventRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="approvalEventRoleViewModel"></param>
        /// <returns></returns>
        IResult ManageApprovalEventRole(ApprovalEventRoleViewModel approvalEventRoleViewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns
        IResult ApprovalTransactionDetails(Guid entityId);
    }
}
