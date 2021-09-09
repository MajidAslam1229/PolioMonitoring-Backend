using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Models.DTO_s
{
    public class FormFilledCount
    {
        public string Name { get; set; }
        public int value { get; set; }

        public string Code { get; set; }
    }

    public class TotalFormFilledCount
    {
        public string FormName { get; set; }
        public int Total { get; set; }

    }

    public class FormFilledCategoryWiseCount
    {
        public string FormName { get; set; }
        public int Total { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }



    }

    public class FormFilledDesignationWiseCount
    {
        public string FormName { get; set; }
        public string Designation { get; set; }
        public string Total { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }

    public class MobileTeamMonitoring
    {
        public string Name { get; set; }
        public int AIClessthan4 { get; set; }
        public int AICgreaterthanequal4 { get; set; }
        public int UCMOlessthan4 { get; set; }
        public int UCMOgreaterthan4 { get; set; }

    }

    public class HouseholdClusterCount
    {
        public string Name { get; set; }
        public int AIClessthan3 { get; set; }
        public int AICgreaterthanequal3 { get; set; }
        public int UCMOlessthan3 { get; set; }
        public int UCMOgreaterthan3 { get; set; }

    }


    public class VaccinatedandUnVaccinatedChildren
    {
        public string Name { get; set; }
        public int zeroto11MChecked { get; set; }
        public int zeroto11Mvaccinated { get; set; }
        public int zeroto11Munvaccinated { get; set; }
        public int eleventto59MChecked { get; set; }

        public int eleventto59vaccinated { get; set; }

        public int eleventto59unvaccinated { get; set; }

    }

    public class MissedChild
    {
        public string Name { get; set; }
        public int TeamMissedthehouse { get; set; }
        public int TVBMC { get; set; }
        public int NA { get; set; }
        public int Ref { get; set; }

        public int Other { get; set; }
    }


    ////////////////////////////////////////////////////New Dashboard///////////////////////////////////
    ///

    #region HomeTab

    public class RegistrationCompaliance
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public int Total { get; set; }
        public int TotalRegistered { get; set; }
    }

    public class Organizationwiseregistration
    {
        public string OrganizationName { get; set; }
        public int Total { get; set; }
    }

    public class AdministrativeComplaince
    {
        public string Name { get; set; }
        public int TotalRegistered { get; set; }
    }

    public class FixedSiteMonitoringTotalFormFilled
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public int Total { get; set; }
    }

    public class FixedSiteMonitoringTotalFormFilledDesigantionWise
    {
        public string Designation { get; set; }

        public int Total { get; set; }
    }

    public class FixedSiteMonitoringFunctionalSIAFixedSite
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public int Functional { get; set; }

        public int NonFunctional { get; set; }

        public int Denominator { get; set; }
    }

    public class SuperVisorTrainedUnTrainedStaff
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Trained { get; set; }
        public int UnTrained { get; set; }

        public int Denominator { get; set; }
    }

    public class UCMOAICHHCluster
    {
        public string Name { get; set; }
        public int AIClessthan3 { get; set; }
        public int AICgreaterthanequal3 { get; set; }
        public int UCMOlessthan3 { get; set; }
        public int UCMOgreaterthan3 { get; set; }

    }

    public class UCMOAICSupervisorCluster
    {
        public string Name { get; set; }
        public int AIClessthan4 { get; set; }
        public int AICgreaterthanequal4 { get; set; }
        public int UCMOlessthan4 { get; set; }
        public int UCMOgreaterthan4 { get; set; }

    }

    public class MobileTeamMonitoringNeapComposition
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int BothAdult { get; set; }
        public int Local { get; set; }
        public int Government { get; set; }

        public int Female { get; set; }

        public int Denominator { get; set; }

    }

    public class MobileTeamMonitoringTrainedUnTrainedStaff
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Trained { get; set; }
        public int UnTrained { get; set; }

        public int Denominator { get; set; }

    }

    public class MobileTeamMonitoringVaccineCondition
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Dry { get; set; }
        public int Cool { get; set; }
        public int ValidVVM { get; set; }

        public int Denominator { get; set; }

    }

    public class TransitTeamMonitoringNeapComposition
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Local { get; set; }
        public int Trained { get; set; }
        public int Adult { get; set; }

        public int Others { get; set; }

        public int Denominator { get; set; }

    }


    public class TransitTeamMonitoringLogisticsForEachMember
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int VaccinceCarrier { get; set; }
        public int FM { get; set; }
        public int TallySheet { get; set; }
        public int Denominator { get; set; }


    }

    public class TransitTeamMonitoringVaccineCondition
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Dry { get; set; }
        public int Cool { get; set; }
        public int ValidVVM { get; set; }

        public int Denominator { get; set; }

    }

    public class TransitTeamMonitoringLEAsSupport
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Present { get; set; }
        public int Helping { get; set; }

        public int Denominator { get; set; }


    }

    public class HouseHoldClusterPopulationType
    {
        public string Name { get; set; }
        public string PopulationType { get; set; }
        public int Code { get; set; }
        public int Total { get; set; }


    }

    public class HouseHoldClusterVaccinatedandUnVaccinatedAgeWise
    {
        public string Name { get; set; }
        public int zeroto11MChecked { get; set; }
        public int zeroto11Mvaccinated { get; set; }
        public int zeroto11Munvaccinated { get; set; }
        public int eleventto59MChecked { get; set; }

        public int eleventto59vaccinated { get; set; }

        public int eleventto59unvaccinated { get; set; }

    }

    public class HouseHoldClusterreasonformissed
    {
        public string Name { get; set; }
        public int TeamMissedthehouse { get; set; }
        public int TVBMC { get; set; }
        public int NA { get; set; }
        public int Ref { get; set; }

        public int Other { get; set; }

        public int Denominator { get; set; }
    }

    public class HouseHoldClusterZeroDoseEIChildren
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Total { get; set; }


    }

    public class CatchUpHouseHoldClusterPopulationType
    {
        public string Name { get; set; }
        public string PopulationType { get; set; }
        public int Code { get; set; }
        public int Total { get; set; }
    }

    public class CatchupHouseHoldChecked
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int zeroByzero { get; set; }
        public int Lock { get; set; }
        public int Ref { get; set; }
        public int NA { get; set; }
    }

    #endregion

    #region Complaince

    public class RegistrationComplianceUClevel
    {
        public string Name { get; set; }
        public int  UCMO { get; set; }

        public int AIC { get; set; }

        public int UCSP { get; set; }
        public int UCPO { get; set; }
        public int UCCO { get; set; }
        public int SM { get; set; }


    }


    public class RegistrationComplianceTehsillevel
    {
        public string Name { get; set; }
        public int TPO { get; set; }

        public int TCO { get; set; }

        public int TSP { get; set; }
        public int DDHO { get; set; }

        public int Other { get; set; }



    }

    public class RegistrationComplianceDistrictlevel
    {
        public string Name { get; set; }
        public int IO { get; set; }

        public int DHCSO { get; set; }

        public int DSO { get; set; }
        public int DHO { get; set; }

        public int CEO { get; set; }



    }

    public class RegistrationComplianceDivisionallevel
    {
        public string Name { get; set; }
        public int AreaCoordinator { get; set; }

        public int DCO { get; set; }



    }

    public class RegistrationComplianceProvincelevel
    {
        public string Name { get; set; }
        public int PEIRISynergyCoordinator { get; set; }

        public int ProvincialCampagnSupportOfficer { get; set; }

        public int ProvincialMonitoringandEvaluationOfficer { get; set; }

        public int ProvincialPolioEradicationOfficer { get; set; }

        public int ProvincialTrainingOfficer { get; set; }



    }

    public class UCMOAICComplaince
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public int UCMO { get; set; }

        public int AIC { get; set; }

    }

    public class OrganizationComplaince
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public int Government { get; set; }

        public int WHO { get; set; }

        public int UNICEF { get; set; }

        public int Other { get; set; }

    }

    #endregion

    #region FixedSite
    public class FixedSiteMonitoringNeap
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public int Local { get; set; }

        public int Adult { get; set; }
        public int Trained { get; set; }
        public int GovernmentAccountable { get; set; }

        public int Denominator { get; set; }
    }

    public class FixedSiteMonitoringVaccineCondition
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public int Dry { get; set; }

        public int Cool { get; set; }

        public int ValidVVM { get; set; }

        public int Denominator { get; set; }
    }

    public class FixedSiteMonitoringTemperatureCondition
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public int NotMaintained { get; set; }

        public int Maintained { get; set; }
        public int Denominator { get; set; }
    }

    public class FixedSiteMonitoringEssentialImmunizationduringSIA
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public int Given { get; set; }

        public int NotGiven { get; set; }

        public int Denominator { get; set; }
    }

    public class FixedSiteMonitoringZeroDoseReferral
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public int NotReferred { get; set; }

        public int Referred { get; set; }

        public int Denominator { get; set; }
    }

    public class FixedSiteMonitoringAFPCaseDefinition
        {
            public string Name { get; set; }

            public int Code { get; set; }

            public int Available { get; set; }

            public int NotAvailable { get; set; }

        public int Denominator { get; set; }
    }
    #endregion

    #region SupervisorMonitoring
    public class SuperVisorNecessaryitems
    {
        public string Name { get; set; }


        public int OPVVial { get; set; }

        public int FM { get; set; }

        public int Chalks { get; set; }

        public int OpsPlan { get; set; }
        public int MClist { get; set; }

        public int MonitoringPlan { get; set; }

        public int HRMPlist { get; set; }

        public int Transport { get; set; }

        public int Denominator { get; set; }
    }

    public class SuperVisorTeamMonitornigchecklist
    {
        public string Name { get; set; }

        public int Correctlyfilled { get; set; }

        public int Incorrect { get; set; }

        public int Denominator { get; set; }

    }

    public class SuperVisorHRMPVisits
    {
        public string Name { get; set; }

        public int yes { get; set; }

        public int No { get; set; }

        public int Denominator { get; set; }

    }

    public class SuperVisorHHClustertaken
    {
        public string Name { get; set; }

        public int yes { get; set; }

        public int No { get; set; }

        public int Denominator { get; set; }

    }
    #endregion

    #region TeamMonitoring
    public class MobileTeamMonitoringDataRecording
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int yes { get; set; }

        public int No { get; set; }

        public int Denominator { get; set; }

    }

    public class MobileTeamMonitoringRouteMaps
    {
        public string Name { get; set; }
        public int Avaiable { get; set; }

        public int NotAvailable { get; set; }

        public int Denominator { get; set; }

    }


    public class MobileTeamMonitoringIPCquestions
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int GoodIPC { get; set; }

        public int PoorIPC { get; set; }

        public int Denominator { get; set; }

    }

    public class MobileTeamMonitoringZeroDoserecording
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Recording { get; set; }

        public int NotRecording { get; set; }

        public int Denominator { get; set; }

    }

    public class TransitTeamMonitoringCNIC
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Valid { get; set; }

        public int NotValid { get; set; }

        public int Denominator { get; set; }

    }
    #endregion

    #region HouseHoldCluster

    public class HouseHoldClusterGuestChildrenVaccination
    {
        public string Name { get; set; }
        public int TotalGuestChildrens { get; set; }
        public int VaccinatedChildrens { get; set; }
    }

    public class HouseHoldClusterFingermarkingstatus
    {
        public string Name { get; set; }
        public int TotalChildrens { get; set; }
        public int FMChildrens { get; set; }
    }
    public class HouseHoldClusterEIVaccinationlessthantwoyears
    {
        public string Name { get; set; }
        public int TotalChildrenslessthantwoyears { get; set; }
        public int ZeroDoselessthentwoyears { get; set; }
    }
    #endregion

    #region CatchUpDaysHouseHoldCluster
    public class CatchupHouseHoldFindings
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Match { get; set; }
        public int NotMatch { get; set; }
    }
    #endregion

    #region Maps
    public class FixedSitePinDrops
    {
        public string FormName { get; set; }
        public string Division { get; set; }

        public string District { get; set; }
        public string Tehsil { get; set; }
        public string UC { get; set; }
        public string Dayofwork { get; set; }
        public int Code { get; set; }
        public Double Lat { get; set; }
        public Double Long { get; set; }

        public string status { get; set; }

        public string TeamNo { get; set; }

        public string Icon { get; set; }
    }

    public class TransitPinDrops
    {
        public string FormName { get; set; }
        public string Division { get; set; }

        public string District { get; set; }
        public string Tehsil { get; set; }
        public string UC { get; set; }
        public string Dayofwork { get; set; }
        public int Code { get; set; }
        public Double Lat { get; set; }
        public Double Long { get; set; }
        public string Icon { get; set; }
    }

    public class ReasonForMissed
    {
        public string FormName { get; set; }
        public string Division { get; set; }

        public string District { get; set; }
        public string Tehsil { get; set; }
        public string UC { get; set; }
        public string Dayofwork { get; set; }
        public int Code { get; set; }
        public Double Lat { get; set; }
        public Double Long { get; set; }

        public string Icon { get; set; }
    }

    #endregion

    #region Reports
    public class CommonClass
    {
        public string Province { get; set; }

        public string Division { get; set; }

        public string District { get; set; }

        public string Tehsil { get; set; }

        public string UC { get; set; }

        public string Dayofwork { get; set; }

        public string Surveyor { get; set; }

        public string Designation { get; set; }

        public DateTime CreationDate { get; set; }

        public string IndicatorName { get; set; }

        public string Answer { get; set; }

    }
    public class FixedSiteReport:CommonClass
    {
   
        public string AIC { get; set; }

         public string TeamNo { get; set; }

    }

    public class Supervisormonitoring : CommonClass
    {

        public string SuperVisorName { get; set; }

    }

    public class TeammonitoringReports : CommonClass
    {

    }

    public class TransitTeammonitoringReports : CommonClass
    {
    }

    public class HouseHoldCluster : CommonClass
    {
        public int zerotoelevenMChecked { get; set; }
        public int zerotoelevenMvaccinated { get; set; }
        public int twelvetofiftynineMChecked { get; set; }
        public int twelvetofiftynineMvaccinated { get; set; }
        public int Teammissedthehouse { get; set; }
        public int TVBMC { get; set; }
        public int NA { get; set; }
        public int Refusals { get; set; }
        public int Others { get; set; }
        public int TotalGuestChildren { get; set; }
        public int VaccinatedGuestChildren { get; set; }
        public int ChildrenUnderTwoYears { get; set; }
        public int AgeMatchedChildren { get; set; }
        public int noofchildrenseenzerotofiftynincemonths { get; set; }
        public int zerotofiftynincemonthschildrenfingermarked { get; set; }
        public int ZeroDose { get; set; }
        public int AFPCase { get; set; }
    }


    public class AdministrativeHouseHoldCluster : CommonClass
    {
        public int TotalChildrenLessthanFiveYears { get; set; }

        public int NoofChildrenFoundFingerMarked { get; set; }

        public int NoofChildrenNotAvailable { get; set; }

        public int NoofChildrenRefused { get; set; }
    }

    public class MonitoringComplaince
    {
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string UC { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }

        public string PhoneNumber { get; set; }

        public string UserLevel { get; set; }

        public string DayofWork { get; set; }

        public int FixedSite { get; set; }

        public int SupervisorMonitoring { get; set; }
        public int TeamMonitoring { get; set; }
        public int TransitTeamMonitoring { get; set; }

        public int HouseHoldCluster { get; set; }

        public int CatchupCluster { get; set; }

    }


    public class RegistrationComplaince
    {

        public string Province { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string UC { get; set; }
        public string Designation { get; set; }
        public string UserLevel { get; set; }
        public int Total { get; set; }
        public int TotalRegistered { get; set; }

    }

    public class HouseHoldClusterMarkers
    {

        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string UC { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

    }

    public class HouseHoldClusterMissedarea
    {
  
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

    }

    public class HouseHoldClusterPoorlyCoveredArea
    {

        public decimal Lat { get; set; }
        public decimal Long { get; set; }

    }
    #endregion
}

