using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppService.Application.Wrappers
{
    public class BaseResponse
    {
        public Guid CorrelationId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
