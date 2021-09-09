using AutoMapper;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.Events
{
    public class EventService
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public EventService(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region AddEvent
        public EventDTO AddEvent(EventDTO model, string UserId)
        {

            using (var db = new PMSDbContext())
            {
                var trans = db.Database.BeginTransaction();
                try
                {
                    if (model.Id == 0 )
                    {
                        var oldEvent = db.PMSEvents.FirstOrDefault(x => x.Status == true);
                        if (oldEvent != null)
                        {
                            oldEvent.Status = false;
                            db.SaveChanges();
                        }

                        var newEvent = this._mapper.Map<PMSEvents>(model);

                        newEvent.StartDate = model.StartDate;
                        newEvent.EndDate = model.EndDate;
                        newEvent.CreatedBy = UserId;
                        newEvent.HideEvent = false;
                        newEvent.CreationDate = DateTime.UtcNow;
                        newEvent.Status = true;

                        db.PMSEvents.Add(newEvent);

                        db.SaveChanges();

                        model.Id = newEvent.Id;
                        model.startdate = model.StartDate.Value.ToString("d/M/yyyy");
                        model.enddate = model.EndDate.Value.ToString("dddd, dd MMMM yyyy");


                    }
                    else
                    {
                        // Map Data of input model
                        var newEvent = this._mapper.Map<PMSEvents>(model);
                        newEvent.UpdatedBy = UserId;
                        newEvent.Status = true;
                        newEvent.HideEvent = false;
                        newEvent.UpdatedDate = UtilService.GetPkCurrentDateTime();
                        db.Entry(newEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();

                    }
                    trans.Commit();
                    model.startdate = model.StartDate.Value.ToString("d/M/yyyy");
                    model.enddate = model.EndDate.Value.ToString("dddd, dd MMMM yyyy");
                    return model;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw; 
                }

            }
        }
        #endregion

        #region GetEventsList
        public PaginationResult<EventDTO> GetEvents(PaginationViewModel model)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var EventsList = db.PMSEvents.Where(x => x.HideEvent == false).ToList();

                    var TotalCount = EventsList.Count();

                    EventsList = EventsList.OrderBy(x => x.Id).Skip((model.Page - 1) * (model.PageSize)).Take(model.PageSize).ToList();

                    var res = this._mapper.Map<List<EventDTO>>(EventsList.ToList());

                    return new PaginationResult<EventDTO> { Data = res, RecordsTotal = TotalCount };

                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region DeleteEventById
        public PMSEvents DeleteEventById(int id)
        {
            try
            {
                using var db = new PMSDbContext();

               
                    var selectedEvent = db.PMSEvents.FirstOrDefault(x => x.Id == id);
                    selectedEvent.HideEvent = true;
                    db.SaveChanges();

                    return selectedEvent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
