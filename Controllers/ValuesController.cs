using System.Web.Http;
using SampleWebAPI.BusinessLogic;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly BulkSmsApi _bulkSmsApi;
        public ValuesController()
        {
            _bulkSmsApi = new BulkSmsApi();
        }

        [HttpPost]
        [Route("bulksms/bulksmsresponse")]
        public BulkSmsResponse BulkSmsResponse(BulkSmsRequest request)
        {
            var response = _bulkSmsApi.BulkSmsResponse(request);
            return response;
        }
    }
}