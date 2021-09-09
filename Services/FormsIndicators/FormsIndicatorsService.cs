using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolioMonitoringSystem.Data.Database.Tables;
using PolioMonitoringSystem.Models.DTO_s;
using PolioMonitoringSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services
{
    public class FormsIndicatorsService
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public FormsIndicatorsService(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region AddFormsIndicator
        public FormsIndicatorDTO AddFormsIndicator(FormsIndicatorDTO model,string userid,string Category)
        {

            using (var db = new PMSDbContext())
            {
                var trans = db.Database.BeginTransaction();
                try
                {
                    model.Id = 0;

                    if (model.Id == 0)
                    {
                        var newFormsIndicator = this._mapper.Map<FormsIndicatorMaster>(model);

                        newFormsIndicator.RecordStatus = true;
                        newFormsIndicator.CreatedBy = userid;
                        newFormsIndicator.CampaignId = UtilService.GetCurrentCampaignId();
                        newFormsIndicator.Category = Category;
                        newFormsIndicator.CreatedDate = UtilService.GetPkCurrentDateTime();


                        db.FormsIndicatorMaster.Add(newFormsIndicator);

                        db.SaveChanges();

                        model.Id = newFormsIndicator.Id;

                        if (model.Id > 0 && model.formsList.Count > 0)
                        {
                            foreach (var formsindicator1 in model.formsList)
                            {
                                foreach (var formsindicator in formsindicator1.formsIndicatorDetailDTOs)
                                {
                                    FormsIndicatorDetails formsIndicatordetail = new FormsIndicatorDetails();

                                    formsindicator.Id = 0;
                                    formsIndicatordetail.FormsIndicatorMasterId = model.Id;
                                    formsIndicatordetail.FormsIndicatorId = formsindicator.FormsIndicatorId;
                                    formsIndicatordetail.SubIdDetail = formsindicator.SubIdDetail;
                                    formsIndicatordetail.Comments = formsindicator.Comments;
                                    formsIndicatordetail.Lat = formsindicator1.Lat;
                                    formsIndicatordetail.Long = formsindicator1.Long;
                                    formsIndicatordetail.RecordStatus = true;
                                    formsIndicatordetail.CreatedBy = userid;
                                    formsIndicatordetail.CreatedDate = UtilService.GetPkCurrentDateTime();

                                    db.FormsIndicatorDetails.Add(formsIndicatordetail);

                                    db.SaveChanges();

                                    formsindicator.Id = formsIndicatordetail.Id;


                                    //////Save Parent Answers in IndicatorAnswers Table
                                    foreach (var ParentAnswer in formsindicator.IndicatorAnswers)
                                    {
                                        IndicatorAnswers indicatorAnswers = new IndicatorAnswers();

                                        if(formsindicator.FormsIndicatorId==155)
                                        {
                                            ParentAnswer.AnswerId = ParentAnswer.FormIndicatorDetailId;
                                        }

                                        indicatorAnswers.FormIndicatorDetailId = formsindicator.Id;
                                        indicatorAnswers.AnswerId = ParentAnswer.AnswerId;
                                        indicatorAnswers.AnswerDesc = ParentAnswer.AnswerDesc;
                                        indicatorAnswers.RecordStatus = true;
                                        indicatorAnswers.CreatedBy = userid;
                                        indicatorAnswers.CreatedDate = UtilService.GetPkCurrentDateTime();

                                        db.IndicatorAnswers.Add(indicatorAnswers);

                                        db.SaveChanges();
                                    }



                                    ///


                                    if (formsindicator.subIndicatorDetailsDTOs.Count > 0)
                                    {
                                        foreach (var subformsindicator in formsindicator.subIndicatorDetailsDTOs)
                                        {
                                            FormsIndicatorDetails subformsindicatordetail = new FormsIndicatorDetails();

                                            subformsindicatordetail.FormsIndicatorMasterId = model.Id;
                                            subformsindicatordetail.FormsIndicatorId = subformsindicator.FormsIndicatorId;
                                            subformsindicatordetail.SubIdDetail = formsindicator.Id;
                                            subformsindicatordetail.Comments = subformsindicator.Comments;
                                            subformsindicatordetail.Lat = formsindicator1.Lat;
                                            subformsindicatordetail.Long = formsindicator1.Long;
                                            subformsindicatordetail.RecordStatus = true;
                                            subformsindicatordetail.CreatedBy = userid;
                                            subformsindicatordetail.CreatedDate = UtilService.GetPkCurrentDateTime();

                                            db.FormsIndicatorDetails.Add(subformsindicatordetail);

                                            db.SaveChanges();

                                            subformsindicatordetail.Id = subformsindicatordetail.Id;

                                            //////Save Child Answers in IndicatorAnswers Table
                                            foreach (var ChildAnswer in subformsindicator.IndicatorAnswers)
                                            {
                                                IndicatorAnswers indicatorAnswers = new IndicatorAnswers();

                                                indicatorAnswers.FormIndicatorDetailId = subformsindicatordetail.Id;
                                                indicatorAnswers.AnswerId = ChildAnswer.AnswerId;
                                                indicatorAnswers.AnswerDesc = ChildAnswer.AnswerDesc;
                                                indicatorAnswers.RecordStatus = true;
                                                indicatorAnswers.CreatedBy = userid;
                                                indicatorAnswers.CreatedDate = UtilService.GetPkCurrentDateTime();

                                                db.IndicatorAnswers.Add(indicatorAnswers);

                                                db.SaveChanges();
                                            }

                                        }
                                    }
                                }

                            }

                        }
                    }
                    else
                    {
                        // Map Data of input model
                        var newFormsIndicator = this._mapper.Map<FormsIndicatorMaster>(model);
                        newFormsIndicator.UpdatedBy = "Admin";
                        newFormsIndicator.RecordStatus = true;
                        newFormsIndicator.UpdatedDate = UtilService.GetPkCurrentDateTime();
                        db.Entry(newFormsIndicator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.SaveChanges();


                        model.Id = newFormsIndicator.Id;

                        if (model.Id > 0 && model.formsList.Count > 0)
                        {
                            foreach (var formsindicator1 in model.formsList)
                            {
                                foreach (var formsindicator in formsindicator1.formsIndicatorDetailDTOs)
                                {
                                    FormsIndicatorDetails formsIndicatordetail = new FormsIndicatorDetails();

                                    if (formsindicator.FormsIndicatorMasterId == 0)
                                    {
                                        formsIndicatordetail.FormsIndicatorMasterId = model.Id;
                                        formsIndicatordetail.FormsIndicatorId = formsindicator.FormsIndicatorId;
                                        formsIndicatordetail.SubIdDetail = formsindicator.SubIdDetail;
                                        formsIndicatordetail.Comments = formsindicator.Comments;
                                        formsIndicatordetail.RecordStatus = true;
                                        formsIndicatordetail.CreatedBy = userid;
                                        formsIndicatordetail.CreatedDate = UtilService.GetPkCurrentDateTime();
                                        db.FormsIndicatorDetails.Add(formsIndicatordetail);
                                        db.SaveChanges();
                                    }

                                    else
                                    {
                                        formsIndicatordetail.FormsIndicatorMasterId = model.Id;
                                        formsIndicatordetail.FormsIndicatorId = formsindicator.FormsIndicatorId;
                                        formsIndicatordetail.FormsIndicatorId = formsindicator.FormsIndicatorId;
                                        formsIndicatordetail.SubIdDetail = formsindicator.SubIdDetail;
                                        formsIndicatordetail.Comments = formsindicator.Comments;
                                        formsIndicatordetail.RecordStatus = true;
                                        formsIndicatordetail.CreatedBy = userid;
                                        formsIndicatordetail.CreatedDate = UtilService.GetPkCurrentDateTime();

                                        db.Entry(newFormsIndicator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        db.SaveChanges();
                                    }

                                }

                            }
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
        public List<FormsIndicatorDTO> GetFormsIndicatorsList()
        {
            try
            {

                using (var db = new PMSDbContext())
                {
                    var formsIndicatorMasters = db.FormsIndicatorMaster.Where(x => x.RecordStatus == true).ToList();

                    var formsIndicatorDTO = this._mapper.Map<List<FormsIndicatorDTO>>(formsIndicatorMasters.ToList());

                    foreach (var formsIndicatordto in formsIndicatorDTO)
                    {

                        List<FormsIndicatorDetailDTO> formsIndicatorDetailDTOs = db.FormsIndicatorDetails.Where(x => x.FormsIndicatorMasterId == formsIndicatordto.Id && x.RecordStatus == true)
                                          .Select(formsIndicatorDetailDTOs => new FormsIndicatorDetailDTO
                                          {
                                              Id = formsIndicatorDetailDTOs.Id,
                                              FormsIndicatorMasterId = formsIndicatorDetailDTOs.FormsIndicatorMasterId,
                                              FormsIndicatorId = formsIndicatorDetailDTOs.FormsIndicatorId,
                                              SubIdDetail = formsIndicatorDetailDTOs.SubIdDetail,
                                              Comments = formsIndicatorDetailDTOs.Comments
                                          }).ToList();

                        formsIndicatordto.formsList[0].formsIndicatorDetailDTOs = formsIndicatorDetailDTOs;

                    }

                    return formsIndicatorDTO;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetFormsIndicatorById
        public GetFormIndicatorDTO GetFormsIndicatorById(int id)
        {
            try
            {

                using (var db = new PMSDbContext())
                {
                    
                    var formsIndicatorMasters = db.FormsIndicatorMaster.Include("Form").Where(x => x.Id == id && x.RecordStatus == true).FirstOrDefault();


                    var formsIndicatorDTO = this._mapper.Map<GetFormIndicatorDTO>(formsIndicatorMasters);

                    var District = db.GEOLVL_V.Where(x => x.PKCODE == formsIndicatorMasters.DistrictCode && x.LVL == "District").Select(x=> new { DistrictName=x.NAME}).FirstOrDefault();

                    var Tehsil = db.GEOLVL_V.Where(x => x.PKCODE == formsIndicatorMasters.TehsilCode && x.LVL=="Tehsil").Select(x => new { TehsilName = x.NAME }).FirstOrDefault();




                    formsIndicatorDTO.FormName = formsIndicatorMasters.Form.FormName;

                    if(District !=null)
                    {
                        formsIndicatorDTO.DistrictName = District.DistrictName.ToString();
                    }

                    if (Tehsil != null)
                    {
                        formsIndicatorDTO.TehsilName = Tehsil.TehsilName.ToString();
                    }





                    ////// Parent Answer


                    List<FormsIndicatorDetailDTO> formsIndicatordetaildTOs = db.FormsIndicatorDetails.Include("FormsIndicator").Where(x => x.FormsIndicatorMasterId == formsIndicatorDTO.Id && x.SubIdDetail == 0 && x.RecordStatus == true)
                                      .Select(formsIndicatorDetailDTOs => new FormsIndicatorDetailDTO
                                      {
                                          Id = formsIndicatorDetailDTOs.Id,
                                          FormsIndicatorMasterId = formsIndicatorDetailDTOs.FormsIndicatorMasterId,
                                          FormsIndicatorName = formsIndicatorDetailDTOs.FormsIndicator.IndicatorName.ToString(),
                                          FormsIndicatorId = formsIndicatorDetailDTOs.FormsIndicatorId,
                                          SubIdDetail = formsIndicatorDetailDTOs.SubIdDetail,
                                          Comments = formsIndicatorDetailDTOs.Comments
                                      }).ToList();

                    formsIndicatorDTO.formsIndicatorDetailDTOs = formsIndicatordetaildTOs;


                    foreach (var parentitem in formsIndicatorDTO.formsIndicatorDetailDTOs)
                    {

                        List<IndicatorAnswerDTO> parentindicatoranswer = db.IndicatorAnswers.Where(x => x.FormIndicatorDetailId == parentitem.Id && x.RecordStatus == true)
                                          .Select(parentindicatoranswer => new IndicatorAnswerDTO
                                          {
                                              Id = parentindicatoranswer.Id,
                                              FormIndicatorDetailId = parentindicatoranswer.FormIndicatorDetailId,
                                              AnswerId = parentindicatoranswer.AnswerId,
                                              AnswerDesc = parentindicatoranswer.AnswerDesc
                                          }).ToList();

                        parentitem.IndicatorAnswers = parentindicatoranswer;
                    }



                    /////Child Answer

                    foreach (var subindicatordetail in formsIndicatorDTO.formsIndicatorDetailDTOs)
                    {

                        List<SubIndicatorDetailsDTO> subindicatordetailsdto = db.FormsIndicatorDetails.Include("FormsIndicator").Where(x => x.SubIdDetail == subindicatordetail.Id && x.RecordStatus == true)
                                          .Select(subindicatordetail => new SubIndicatorDetailsDTO
                                          {
                                              Id = subindicatordetail.Id,
                                              FormsIndicatorId = subindicatordetail.FormsIndicatorId,
                                              IndicatorName = subindicatordetail.FormsIndicator.IndicatorName.ToString(),
                                              SubIdDetail = subindicatordetail.SubIdDetail,
                                              Comments = subindicatordetail.Comments

                                          }).ToList();

                        subindicatordetail.subIndicatorDetailsDTOs = subindicatordetailsdto;

                        foreach (var childitem in subindicatordetail.subIndicatorDetailsDTOs)
                        {

                            List<IndicatorAnswerDTO> childindicatoranswer = db.IndicatorAnswers.Where(x => x.FormIndicatorDetailId == childitem.Id && x.RecordStatus == true)
                                              .Select(parentindicatoranswer => new IndicatorAnswerDTO
                                              {
                                                  Id = parentindicatoranswer.Id,
                                                  FormIndicatorDetailId = parentindicatoranswer.FormIndicatorDetailId,
                                                  AnswerId = parentindicatoranswer.AnswerId,
                                                  AnswerDesc = parentindicatoranswer.AnswerDesc
                                              }).ToList();

                            childitem.IndicatorAnswers = childindicatoranswer;
                        }

                    }


                    return formsIndicatorDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region DeleteFormIndicatorsById
        //public FormsIndicator DeleteFormIndicatorsById(int id)
        //{
        //    try
        //    {
        //        using var db = new PMSDbContext();

        //        var formsIndicator = db.FormsIndicator.FirstOrDefault(x => x.Id == id);
        //        formsIndicator.RecordStatus = false;
        //        db.SaveChanges();

        //        return formsIndicator;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion
    }
}
