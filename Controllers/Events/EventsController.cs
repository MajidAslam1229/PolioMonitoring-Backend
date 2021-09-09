
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using PolioMonitoringSystem.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Controllers.Events
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private EventService _eventServices;

        public EventController(IMapper mapper)
        {
            _eventServices = new EventService(mapper);

        }

        #region AddEvent
        [HttpPost]
        [Route("AddEvent")]
        public IActionResult AddEvent([FromBody] EventDTO model)
        {
            try
            {
                ///Get userid
               
                string userid = this.GetUserId();

                var res = _eventServices.AddEvent(model, userid);

                return Ok(UtilService.GetResponse(res));

            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion


        #region GetEventsList
        
        [HttpPost]
        [Route("GetEvents")]
        public IActionResult GetEvents([FromBody] PaginationViewModel model)
        {
            try
            {
                var EventsList = _eventServices.GetEvents(model);

                return Ok(new { EventsList.Data, EventsList.RecordsTotal });
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion

        #region DeleteEventById
        [HttpDelete]
        [Route("DeleteEventById/{Id}")]
        public IActionResult DeleteEventById(int id)
        {
            try
            {
                PMSEvents events = _eventServices.DeleteEventById(id);
                if (events == null)
                {
                    return Ok(UtilService.GetExResponse<PMSEvents>(new Exception("Record not found")));
                }
                return Ok(UtilService.GetExResponse<PMSEvents>(new Exception("Record Deleted Successfully")));
            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
    }
}
