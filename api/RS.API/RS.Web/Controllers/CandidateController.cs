using Microsoft.AspNetCore.Mvc;
using RS.Common.CommonData;
using RS.Common.Enums;
using RS.ViewModel.Roles;
using RS.Service.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using RS.ViewModel.Candidate;
using System.Threading.Tasks;

namespace RS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Candidate/[Action]")]
    [Authorize]
    public class CandidateController : Controller
    {
        private readonly ICandidateManagerService _candidateManagerService;

        public CandidateController(ICandidateManagerService candidateManager)
        {
            _candidateManagerService = candidateManager;

        }

        [ValidateModel]
        [HttpPost]
        public IResult AddCandidate([FromBody]CandidateViewModel candidateView)
        {
            var addedCandidate = _candidateManagerService.AddCandidate(candidateView);
            return addedCandidate;
        }

        [ValidateModel]
        [HttpPut]
        public IResult UpdateCandidate([FromBody]CandidateViewModel candidateView)
        {
            var updatedCandidate = _candidateManagerService.UpdateCandidate(candidateView);
            return updatedCandidate;
        }

        [HttpGet]
        public IResult GetAllCandidate()
        {
            var allCandidates = _candidateManagerService.GetAllCandidate();
            return allCandidates;
        }

        [HttpGet]
        public IResult GetCandidateById(Guid id)
        {
            var candidateRecord = _candidateManagerService.GetCandidateById(id);
            return candidateRecord;
        }

        [HttpPost]
        public Task<IResult> UploadResume()
        {
            var file = Request.Form.Files["uploadFile"];
            var candidateRecord = _candidateManagerService.UploadDocumentAsync(file);
            return candidateRecord;
        }
    }
}