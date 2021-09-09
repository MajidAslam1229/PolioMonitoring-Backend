using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.FormSettings
{
    public class FormsIndicatorSettingsService
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public FormsIndicatorSettingsService(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region AddFormsIndicator
        public FormsIndicatorSettingsDTO AddFormsIndicator(FormsIndicatorSettingsDTO model, string UserId)
        {

            using (var db = new PMSDbContext())
            {
                var trans = db.Database.BeginTransaction();
                try
                {
                    if (model.Id == 0)
                    {
                        ////Mapping Values
                        var newFormsIndicator = this._mapper.Map<FormsIndicator>(model);
                        //////


                        newFormsIndicator.RecordStatus = true;
                        newFormsIndicator.CreatedBy = UserId;
                        newFormsIndicator.CreatedDate = UtilService.GetPkCurrentDateTime();
                        


                        db.FormsIndicator.Add(newFormsIndicator);

                        db.SaveChanges();

                        model.Id = newFormsIndicator.Id;


                        /////Add Indicator Options List in IndicatorOptions table

                        if (model.optionList.Count > 0 && model.Id > 0)
                        {
                            foreach (var item in model.optionList)
                            {

                                IndicatorOptions indicatorOptions = new IndicatorOptions();

                                indicatorOptions.IndicatorId = model.Id;
                                indicatorOptions.Label = item.Label;
                                indicatorOptions.InputType = item.InputType;
                                indicatorOptions.ForComments = item.ForComments;
                                indicatorOptions.ForSubindicator = item.ForSubindicator;
                                indicatorOptions.RecordStatus = true;
                                indicatorOptions.CreatedBy = UserId;
                                indicatorOptions.CreatedDate = UtilService.GetPkCurrentDateTime();

                                db.IndicatorOptions.Add(indicatorOptions);

                                db.SaveChanges();

                            }
                        }


                        ///


                        ///////Save Child Indicator in database

                        if (model.HavingSubIndicator == true)
                        {
                            foreach (var formsindicator in model.SubIndicatorListDTOs)
                            {
                                FormsIndicator formsIndicator = new FormsIndicator();
                                formsIndicator.ParentIndicatorId = model.Id;
                                formsIndicator.IndicatorName = formsindicator.IndicatorName;
                                formsIndicator.Type = formsindicator.inputType;
                                formsIndicator.Isrequired = formsindicator.Isrequired;
                                formsIndicator.Comments = formsindicator.Comments;
                                formsIndicator.SubIndicatorDependency = formsindicator.SubIndicatorDependency;
                                formsIndicator.ParentIndicatorId = model.Id;
                                formsIndicator.RecordStatus = true;
                                formsIndicator.CreatedBy = UserId;
                                formsIndicator.CreatedDate = UtilService.GetPkCurrentDateTime();

                                db.FormsIndicator.Add(formsIndicator);

                                db.SaveChanges();

                                formsindicator.Id = formsIndicator.Id;


                                /////////////Save the Child Indicator OptionList in database at here

                                foreach (var item in formsindicator.optionList)
                                {

                                    IndicatorOptions indicatorOptions = new IndicatorOptions();

                                    indicatorOptions.IndicatorId = formsindicator.Id;
                                    indicatorOptions.Label = item.Label;
                                    indicatorOptions.InputType = item.InputType;
                                    indicatorOptions.ForComments = item.ForComments;
                                    indicatorOptions.ForSubindicator = item.ForSubindicator;
                                    indicatorOptions.RecordStatus = true;
                                    indicatorOptions.CreatedBy = UserId;
                                    indicatorOptions.CreatedDate = UtilService.GetPkCurrentDateTime();

                                    db.IndicatorOptions.Add(indicatorOptions);

                                    db.SaveChanges();

                                }


                                ////////////////////////

                            }


                        }


                    }
                    else
                    {
                        // Map Data of input model
                        var newFormsIndicator = this._mapper.Map<FormsIndicator>(model);
                        newFormsIndicator.UpdatedBy = UserId;
                        newFormsIndicator.RecordStatus = true;
                        newFormsIndicator.UpdatedDate = UtilService.GetPkCurrentDateTime();
                        db.Entry(newFormsIndicator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();


                        /////Update Indicator Options List in IndicatorOptions table

                        if (model.optionList.Count > 0 && model.Id > 0)
                        {

                            ///Remove the Previos OptionList
                            if(model.optionListToRemove.Count>0)
                            {

                                foreach(var item in model.optionListToRemove)
                                {
                                    var indicatorOptions = db.IndicatorOptions.Where(x => x.Id == item.Id).FirstOrDefault();

                                    if(indicatorOptions!=null)
                                    {
                                        indicatorOptions.RecordStatus = false;

                                        db.SaveChanges();
                                    }

                                  
                                }
                               


                            }
                            ///


                            foreach (var item in model.optionList)
                            {

                                if (item.Id == 0)
                                {
                                    IndicatorOptions indicatorOptions = new IndicatorOptions();

                                    indicatorOptions.IndicatorId = model.Id;
                                    indicatorOptions.Label = item.Label;
                                    indicatorOptions.InputType = item.InputType;
                                    indicatorOptions.ForComments = item.ForComments;
                                    indicatorOptions.ForSubindicator = item.ForSubindicator;
                                    indicatorOptions.RecordStatus = true;
                                    indicatorOptions.CreatedBy = UserId;
                                    indicatorOptions.CreatedDate = UtilService.GetPkCurrentDateTime();

                                    db.IndicatorOptions.Add(indicatorOptions);

                                    db.SaveChanges();
                                }
                                else
                                {
                                    IndicatorOptions indicatorOptions = new IndicatorOptions();

                                    indicatorOptions.Id = item.Id;
                                    indicatorOptions.IndicatorId = model.Id;
                                    indicatorOptions.Label = item.Label;
                                    indicatorOptions.InputType = item.InputType;
                                    indicatorOptions.ForComments = item.ForComments;
                                    indicatorOptions.ForSubindicator = item.ForSubindicator;
                                    indicatorOptions.RecordStatus = true;
                                    indicatorOptions.UpdatedBy = UserId;
                                    indicatorOptions.UpdatedDate = UtilService.GetPkCurrentDateTime();

                                    db.Entry(indicatorOptions).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                    db.SaveChanges();
                                }

                            }
                        }


                        ///




                        if (model.HavingSubIndicator == true)
                        {
                            foreach (var formsindicator in model.SubIndicatorListDTOs)
                            {
                                if (formsindicator.Id == 0)
                                {
                                    FormsIndicator formsIndicator = new FormsIndicator();
                                    formsIndicator.ParentIndicatorId = model.Id;
                                    formsIndicator.Comments = formsindicator.Comments;
                                    formsIndicator.IndicatorName = formsindicator.IndicatorName;
                                    formsIndicator.Type = formsindicator.inputType;
                                    formsIndicator.SubIndicatorDependency = formsindicator.SubIndicatorDependency;
                                    formsIndicator.Isrequired = formsindicator.Isrequired;
                                    formsIndicator.ParentIndicatorId = model.Id;
                                    formsIndicator.RecordStatus = true;
                                    formsIndicator.CreatedBy = UserId;
                                    formsIndicator.CreatedDate = UtilService.GetPkCurrentDateTime();
                                    db.FormsIndicator.Add(formsIndicator);
                                    db.SaveChanges();
                                    formsindicator.Id = formsIndicator.Id;
                                    /////Update Indicator Options List in IndicatorOptions table

                                    if (model.optionList.Count > 0 && model.Id > 0)
                                    {
                                        foreach (var item in formsindicator.optionList)
                                        {
                                            ///Remove the Previos OptionList
                                            if (model.optionListToRemove.Count > 0)
                                            {

                                                foreach (var childitem in model.optionListToRemove)
                                                {
                                                    var indicatorOptions = db.IndicatorOptions.Where(x => x.Id == childitem.Id).FirstOrDefault();

                                                    if (indicatorOptions != null)
                                                    {
                                                        indicatorOptions.RecordStatus = false;

                                                        db.SaveChanges();
                                                    }
                                                }

                                            }
                                            ///


                                            if (item.Id == 0)
                                            {
                                                IndicatorOptions indicatorOptions = new IndicatorOptions();

                                                indicatorOptions.IndicatorId = formsindicator.Id;
                                                indicatorOptions.Label = item.Label;
                                                indicatorOptions.InputType = item.InputType;
                                                indicatorOptions.ForComments = item.ForComments;
                                                indicatorOptions.ForSubindicator = item.ForSubindicator;
                                                indicatorOptions.RecordStatus = true;
                                                indicatorOptions.CreatedBy = UserId;
                                                indicatorOptions.CreatedDate = UtilService.GetPkCurrentDateTime();

                                                db.IndicatorOptions.Add(indicatorOptions);

                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                IndicatorOptions indicatorOptions = new IndicatorOptions();

                                                indicatorOptions.Id = item.Id;
                                                indicatorOptions.IndicatorId = formsindicator.Id;
                                                indicatorOptions.Label = item.Label;
                                                indicatorOptions.InputType = item.InputType;
                                                indicatorOptions.ForComments = item.ForComments;
                                                indicatorOptions.ForSubindicator = item.ForSubindicator;
                                                indicatorOptions.RecordStatus = true;
                                                indicatorOptions.UpdatedBy = UserId;
                                                indicatorOptions.UpdatedDate = UtilService.GetPkCurrentDateTime();

                                                db.Entry(indicatorOptions).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                                db.SaveChanges();
                                            }

                                        }
                                    }

                                }
                                else
                                {
                                    FormsIndicator formsIndicator = new FormsIndicator();
                                    formsIndicator.Id = formsindicator.Id;
                                    formsIndicator.ParentIndicatorId = model.Id;
                                    formsIndicator.IndicatorName = formsindicator.IndicatorName;
                                    formsIndicator.Type = formsindicator.inputType;
                                    formsIndicator.SubIndicatorDependency = formsindicator.SubIndicatorDependency;
                                    formsIndicator.Isrequired = formsindicator.Isrequired;
                                    formsIndicator.Comments = formsindicator.Comments;
                                    formsIndicator.ParentIndicatorId = model.Id;
                                    formsIndicator.RecordStatus = true;
                                    formsIndicator.CreatedBy = UserId;
                                    formsIndicator.CreatedDate = UtilService.GetPkCurrentDateTime();

                                    /////Update Indicator Options List in IndicatorOptions table

                                    if (formsindicator.optionList.Count > 0 && model.Id > 0)
                                    {
                                        foreach (var item in formsindicator.optionList)
                                        {

                                            if (item.Id == 0)
                                            {
                                                IndicatorOptions indicatorOptions = new IndicatorOptions();

                                                indicatorOptions.IndicatorId = formsindicator.Id;
                                                indicatorOptions.Label = item.Label;
                                                indicatorOptions.InputType = item.InputType;
                                                indicatorOptions.ForComments = item.ForComments;
                                                indicatorOptions.ForSubindicator = item.ForSubindicator;
                                                indicatorOptions.RecordStatus = true;
                                                indicatorOptions.CreatedBy = UserId;
                                                indicatorOptions.CreatedDate = UtilService.GetPkCurrentDateTime();

                                                db.IndicatorOptions.Add(indicatorOptions);

                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                IndicatorOptions indicatorOptions = new IndicatorOptions();

                                                indicatorOptions.Id = item.Id;
                                                indicatorOptions.IndicatorId = item.ParentIndicatorId;
                                                indicatorOptions.Label = item.Label;
                                                indicatorOptions.InputType = item.InputType;
                                                indicatorOptions.ForComments = item.ForComments;
                                                indicatorOptions.ForSubindicator = item.ForSubindicator;
                                                indicatorOptions.RecordStatus = true;
                                                indicatorOptions.UpdatedBy = UserId;
                                                indicatorOptions.UpdatedDate = UtilService.GetPkCurrentDateTime();

                                                db.Entry(indicatorOptions).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                                db.SaveChanges();
                                            }

                                        }
                                    }


                                    ///
                                    db.Entry(formsIndicator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    db.SaveChanges();

                                }


                            }
                            db.SaveChanges();

                        }


                    }

                    trans.Commit();

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

        #region GetFormsIndicatorsList
        public List<FormsIndicatorSettingsDTO> GetFormsIndicatorsList()
        {
            try
            {
                List<FormsIndicatorSettingsDTO> formsIndicatorDTO;
                using (var db = new PMSDbContext())
                {
                    formsIndicatorDTO = db.FormsIndicator.Where(x => x.ParentIndicatorId == null && x.RecordStatus == true)
                   .Select(formsIndicator => new FormsIndicatorSettingsDTO
                   {
                       Id = formsIndicator.Id,
                       IndicatorName = formsIndicator.IndicatorName,
                       Type = formsIndicator.Type,
                       Comments = formsIndicator.Comments,
                       Isrequired = formsIndicator.Isrequired,
                       IndicatorCategory = formsIndicator.IndicatorCategory,
                       FormId = formsIndicator.FormId,
                       HavingSubIndicator = formsIndicator.HavingSubIndicator,
                       SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                   }).ToList();

                    foreach (var formsIndicator in formsIndicatorDTO)
                    {

                        List<OptionList> optionListDTOs = db.IndicatorOptions.Where(x => x.IndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                          .Select(optionListDTOs => new OptionList
                                          {
                                              Id = optionListDTOs.Id,
                                              ParentIndicatorId = optionListDTOs.IndicatorId,
                                              Label = optionListDTOs.Label,
                                              InputType = optionListDTOs.InputType



                                          }).ToList();

                        formsIndicator.optionList = optionListDTOs;

                    }

                    foreach (var formsIndicator in formsIndicatorDTO)
                    {
                        if (formsIndicator.HavingSubIndicator == true)
                        {
                            List<SubIndicatorList> subIndicatorListDTOs = db.FormsIndicator.Where(x => x.ParentIndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                              .Select(subIndicatorListDTOs => new SubIndicatorList
                                              {
                                                  Id = subIndicatorListDTOs.Id,
                                                  ParentIndicatorId = subIndicatorListDTOs.ParentIndicatorId,
                                                  Type = subIndicatorListDTOs.Type,
                                                  Isrequired = subIndicatorListDTOs.Isrequired,
                                                  IndicatorName = subIndicatorListDTOs.IndicatorName,
                                                  Comments = subIndicatorListDTOs.Comments,
                                                  SubIndicatorDependency = subIndicatorListDTOs.SubIndicatorDependency


                                              }).ToList();

                            formsIndicator.SubIndicatorListDTOs = subIndicatorListDTOs;
                        }
                    }




                }
                return formsIndicatorDTO;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region GetFormsIndicatorById
        public FormsIndicatorSettingsDTO GetFormsIndicatorById(int id)
        {
            try
            {

                using var db = new PMSDbContext();

                FormsIndicatorSettingsDTO formsIndicator = db.FormsIndicator.Where(x => x.Id == id && x.RecordStatus == true)
                    .Select(formsIndicator => new FormsIndicatorSettingsDTO
                    {
                        Id = formsIndicator.Id,
                        IndicatorName = formsIndicator.IndicatorName,
                        Type = formsIndicator.Type,
                        Isrequired = formsIndicator.Isrequired,
                        Comments = formsIndicator.Comments,
                        IndicatorCategory = formsIndicator.IndicatorCategory,
                        FormId = formsIndicator.FormId,
                        HavingSubIndicator = formsIndicator.HavingSubIndicator,
                        SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                    }).FirstOrDefault();


                List<OptionList> optionListDTOs = db.IndicatorOptions.Where(x => x.IndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                  .Select(optionListDTOs => new OptionList
                                  {
                                      Id = optionListDTOs.Id,
                                      ParentIndicatorId = optionListDTOs.IndicatorId,
                                      Label = optionListDTOs.Label,
                                      InputType = optionListDTOs.InputType,
                                      ForComments = optionListDTOs.ForComments,
                                      ForSubindicator = optionListDTOs.ForSubindicator
                                  }).ToList();

                formsIndicator.optionList = optionListDTOs;



                if (formsIndicator.HavingSubIndicator == true)
                {
                    List<SubIndicatorList> subIndicatorListDTOs = db.FormsIndicator.Where(x => x.ParentIndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                      .Select(subIndicatorListDTOs => new SubIndicatorList
                                      {
                                          Id = subIndicatorListDTOs.Id,
                                          ParentIndicatorId = subIndicatorListDTOs.ParentIndicatorId,
                                          inputType = subIndicatorListDTOs.Type,
                                          Type = subIndicatorListDTOs.Type,
                                          Isrequired = subIndicatorListDTOs.Isrequired,
                                          Comments = subIndicatorListDTOs.Comments,
                                          IndicatorName = subIndicatorListDTOs.IndicatorName,
                                          SubIndicatorDependency = subIndicatorListDTOs.SubIndicatorDependency


                                      }).ToList();


                    foreach (var item in subIndicatorListDTOs)
                    {
                        List<OptionList> childoptionListDTOs = db.IndicatorOptions.Where(x => x.IndicatorId == item.Id && x.RecordStatus == true)
                                          .Select(optionListDTOs => new OptionList
                                          {
                                              Id = optionListDTOs.Id,
                                              ParentIndicatorId = optionListDTOs.IndicatorId,
                                              Label = optionListDTOs.Label,
                                              InputType = optionListDTOs.InputType,
                                              ForComments = optionListDTOs.ForComments,
                                              ForSubindicator = optionListDTOs.ForSubindicator
                                          }).ToList();

                        item.optionList = childoptionListDTOs;

                        formsIndicator.SubIndicatorListDTOs = subIndicatorListDTOs;

                    }


                }

                return formsIndicator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteFormIndicatorsById
        public FormsIndicator DeleteFormIndicatorsById(int id)
        {
            try
            {
                using var db = new PMSDbContext();

                var formsIndicator = db.FormsIndicator.FirstOrDefault(x => x.Id == id);
                formsIndicator.RecordStatus = false;
                db.SaveChanges();

                return formsIndicator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteFormIndicatorsOptionsById
        public IndicatorOptions DeleteFormIndicatorsOptionsById(int id)
        {
            try
            {
                using var db = new PMSDbContext();


                var indicatorOptions = db.IndicatorOptions.FirstOrDefault(x => x.Id == id);
                indicatorOptions.RecordStatus = false;
                db.SaveChanges();

                return indicatorOptions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SearchIndicatorsByFormId
        public List<FormsIndicatorSettingsDTO> SearchIndicatorsByFormId(int Id, string value)
        {
            try
            {
                List<FormsIndicatorSettingsDTO> formsIndicatorDTO;
                using (var db = new PMSDbContext())
                {
                    if (value == "All" && Id != 0)
                    {
                        formsIndicatorDTO = db.FormsIndicator.Where(x => x.ParentIndicatorId == null && x.RecordStatus == true &&
                    x.FormId == Id).Select(formsIndicator => new FormsIndicatorSettingsDTO
                    {
                        Id = formsIndicator.Id,
                        IndicatorName = formsIndicator.IndicatorName,
                        Type = formsIndicator.Type,
                        Isrequired = formsIndicator.Isrequired,
                        Comments = formsIndicator.Comments,
                        IndicatorCategory = formsIndicator.IndicatorCategory,
                        FormId = formsIndicator.FormId,
                        HavingSubIndicator = formsIndicator.HavingSubIndicator,
                        SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                    }).ToList();
                    }
                    else if (value == "All" && Id == 0)
                    {
                        formsIndicatorDTO = db.FormsIndicator.Where(x => x.ParentIndicatorId == null && x.RecordStatus == true
                        ).Select(formsIndicator => new FormsIndicatorSettingsDTO
                        {
                            Id = formsIndicator.Id,
                            IndicatorName = formsIndicator.IndicatorName,
                            Type = formsIndicator.Type,
                            Isrequired = formsIndicator.Isrequired,
                            Comments = formsIndicator.Comments,
                            IndicatorCategory = formsIndicator.IndicatorCategory,
                            FormId = formsIndicator.FormId,
                            HavingSubIndicator = formsIndicator.HavingSubIndicator,
                            SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                        }).ToList();
                    }
                    else if (value != "All" && Id == 0)
                    {
                        formsIndicatorDTO = db.FormsIndicator.Where(x => x.ParentIndicatorId == null && x.RecordStatus == true
                        && x.IndicatorCategory == value).Select(formsIndicator => new FormsIndicatorSettingsDTO
                        {
                            Id = formsIndicator.Id,
                            IndicatorName = formsIndicator.IndicatorName,
                            Type = formsIndicator.Type,
                            Isrequired = formsIndicator.Isrequired,
                            Comments = formsIndicator.Comments,
                            IndicatorCategory = formsIndicator.IndicatorCategory,
                            FormId = formsIndicator.FormId,
                            HavingSubIndicator = formsIndicator.HavingSubIndicator,
                            SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                        }).ToList();
                    }
                    else
                    {
                        formsIndicatorDTO = db.FormsIndicator.Where(x => x.ParentIndicatorId == null && x.RecordStatus == true &&
                    x.FormId == Id && x.IndicatorCategory == value).Select(formsIndicator => new FormsIndicatorSettingsDTO
                    {
                        Id = formsIndicator.Id,
                        IndicatorName = formsIndicator.IndicatorName,
                        Type = formsIndicator.Type,
                        Isrequired = formsIndicator.Isrequired,
                        Comments = formsIndicator.Comments,
                        IndicatorCategory = formsIndicator.IndicatorCategory,
                        FormId = formsIndicator.FormId,
                        HavingSubIndicator = formsIndicator.HavingSubIndicator,
                        SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                    }).ToList();
                    }
                    foreach (var formsIndicator in formsIndicatorDTO)
                    {

                        List<OptionList> optionListDTOs = db.IndicatorOptions.Where(x => x.IndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                          .Select(optionListDTOs => new OptionList
                                          {
                                              Id = optionListDTOs.Id,
                                              ParentIndicatorId = optionListDTOs.IndicatorId,
                                              Label = optionListDTOs.Label,
                                              InputType = optionListDTOs.InputType


                                          }).ToList();



                        formsIndicator.optionList = optionListDTOs;

                    }

                    foreach (var formsIndicator in formsIndicatorDTO)
                    {
                        if (formsIndicator.HavingSubIndicator == true)
                        {
                            List<SubIndicatorList> subIndicatorListDTOs = db.FormsIndicator.Where(x => x.ParentIndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                              .Select(subIndicatorListDTOs => new SubIndicatorList
                                              {
                                                  Id = subIndicatorListDTOs.Id,
                                                  ParentIndicatorId = subIndicatorListDTOs.ParentIndicatorId,
                                                  IndicatorName = subIndicatorListDTOs.IndicatorName,
                                                  Type = subIndicatorListDTOs.Type,
                                                  Isrequired = subIndicatorListDTOs.Isrequired,
                                                  Comments = subIndicatorListDTOs.Comments,
                                                  SubIndicatorDependency = subIndicatorListDTOs.SubIndicatorDependency
                                              }).ToList();

                            formsIndicator.SubIndicatorListDTOs = subIndicatorListDTOs;
                        }
                    }




                }
                return formsIndicatorDTO;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region GetFormsIndicatorByFormId
        public List<GetFormsIndicatorSettingsDTO> GetFormsIndicatorByFormId(int id,string Category)
        {
            try
            {
                List<GetFormsIndicatorSettingsDTO> formsIndicatorDTO;

                using (var db = new PMSDbContext())
                {
                    formsIndicatorDTO = db.FormsIndicator.Where(x => x.FormId == id && x.ParentIndicatorId == null && x.IndicatorCategory == Category && x.RecordStatus == true).OrderBy(x=>x.OrderNo)
                   .Select(formsIndicator => new GetFormsIndicatorSettingsDTO
                   {
                       Id = formsIndicator.Id,
                       OrderNo=formsIndicator.OrderNo,
                       Type = formsIndicator.Type,
                       IndicatorName = formsIndicator.IndicatorName,
                       Isrequired = formsIndicator.Isrequired,
                       Comments = formsIndicator.Comments,
                       IndicatorCategory = formsIndicator.IndicatorCategory,
                       FormId = formsIndicator.FormId,
                       HavingSubIndicator = formsIndicator.HavingSubIndicator,
                       SubIndicatorDependency = formsIndicator.SubIndicatorDependency,

                   }).ToList();


                    foreach (var formsIndicator in formsIndicatorDTO)
                    {

                        List<OptionList> optionListDTOs = db.IndicatorOptions.Where(x => x.IndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                          .Select(optionListDTOs => new OptionList
                                          {
                                              Id = optionListDTOs.Id,
                                              ParentIndicatorId = optionListDTOs.IndicatorId,
                                              Label = optionListDTOs.Label,
                                              InputType = optionListDTOs.InputType,
                                              ForComments = optionListDTOs.ForComments,
                                              ForSubindicator = optionListDTOs.ForSubindicator


                                          }).ToList();

                        ////Mapping Values
                        var newoptionListDTO = this._mapper.Map<List<GetOptionListDTO>>(optionListDTOs);
                        //////

                        formsIndicator.optionList = newoptionListDTO;

                        ///////////////////Get List item of dropdown for which comments and SubIndicator will be true

                        if (optionListDTOs.Count > 0)
                        {
                            foreach (var item in optionListDTOs)
                            {
                                /////Check on which label, Comments will be true 
                                if (item.ForComments == true)
                                {
                                    formsIndicator.CommentsForOptionList = item.Id;
                                }
                                /////Check on which label, subindicators will be show
                                ///
                                if (item.ForSubindicator == true)
                                {
                                    formsIndicator.SubIndicatorForOptionList = item.Id;
                                }

                            }
                        }

                        /////////////////////////////

                    }


                    foreach (var formsIndicator in formsIndicatorDTO)
                    {
                        if (formsIndicator.HavingSubIndicator == true)
                        {
                            List<GetSubIndicatorList> subIndicatorListDTOs = db.FormsIndicator.Where(x => x.ParentIndicatorId == formsIndicator.Id && x.RecordStatus == true)
                                              .Select(subIndicatorListDTOs => new GetSubIndicatorList
                                              {
                                                  Id = subIndicatorListDTOs.Id,
                                                  ParentIndicatorId = subIndicatorListDTOs.ParentIndicatorId,
                                                  IndicatorName = subIndicatorListDTOs.IndicatorName,
                                                  Type = subIndicatorListDTOs.Type,
                                                  Isrequired = subIndicatorListDTOs.Isrequired,
                                                  Comments = subIndicatorListDTOs.Comments,
                                                  SubIndicatorDependency = subIndicatorListDTOs.SubIndicatorDependency


                                              }).ToList();

                            formsIndicator.SubIndicatorListDTOs = subIndicatorListDTOs;

                            ///////////////////Get List item of dropdown for which comments and SubIndicator will be true

                            foreach (var childitem in subIndicatorListDTOs)
                            {

                                List<OptionList> optionListDTOs = db.IndicatorOptions.Where(x => x.IndicatorId == childitem.Id && x.RecordStatus == true)
                                                  .Select(optionListDTOs => new OptionList
                                                  {
                                                      Id = optionListDTOs.Id,
                                                      ParentIndicatorId = optionListDTOs.IndicatorId,
                                                      Label = optionListDTOs.Label,
                                                      InputType = optionListDTOs.InputType,
                                                      ForComments = optionListDTOs.ForComments,
                                                      ForSubindicator = optionListDTOs.ForSubindicator

                                                  }).ToList();

                                ////Mapping Values
                                var newoptionListDTO = this._mapper.Map<List<GetOptionListDTO>>(optionListDTOs);
                                //////

                                childitem.optionList = newoptionListDTO;

                                ///////////////////Get List item of dropdown for which comments and SubIndicator will be true

                                if (optionListDTOs.Count > 0)
                                {
                                    foreach (var item in optionListDTOs)
                                    {
                                        /////Check on which label, Comments will be true 
                                        if (item.ForComments == true)
                                        {
                                            childitem.CommentsForOptionList = item.Id;
                                        }


                                    }
                                }

                                /////////////////////////////

                            }


                            /////////////////////////////
                        }
                    }




                }
                return formsIndicatorDTO;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
