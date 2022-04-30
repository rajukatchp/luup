﻿using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ActionsAPI.Models
{    
    public class Value
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }
        public string mobilephone { get; set; }
        public string address1_country { get; set; }
        public bool merged { get; set; }
        public int prioritycode { get; set; }
        public bool confirminterest { get; set; }
        public double? exchangerate { get; set; }
        public string _parentaccountid_value { get; set; }
        public bool decisionmaker { get; set; }
        public DateTime modifiedon { get; set; }
        public double? revenue_base { get; set; }
        public string _owninguser_value { get; set; }
        public int address1_shippingmethodcode { get; set; }
        public string address1_composite { get; set; }
        public string lastname { get; set; }
        public bool donotpostalmail { get; set; }
        public int? numberofemployees { get; set; }
        public bool donotphone { get; set; }
        public int preferredcontactmethodcode { get; set; }
        public string _ownerid_value { get; set; }
        public string sic { get; set; }
        public string firstname { get; set; }
        public bool evaluatefit { get; set; }
        public string yomifullname { get; set; }
        public int address2_addresstypecode { get; set; }
        public bool donotemail { get; set; }
        public int address2_shippingmethodcode { get; set; }
        public string fullname { get; set; }
        public string address1_addressid { get; set; }
        public bool msdyn_gdproptout { get; set; }
        public int statuscode { get; set; }
        public DateTime createdon { get; set; }
        public string address1_stateorprovince { get; set; }
        public string _msdyn_predictivescoreid_value { get; set; }
        public string companyname { get; set; }
        public bool donotfax { get; set; }
        public int? leadsourcecode { get; set; }
        public string jobtitle { get; set; }
        public int versionnumber { get; set; }
        public string address1_line1 { get; set; }
        public string emailaddress1 { get; set; }
        public string telephone1 { get; set; }
        public bool donotsendmm { get; set; }
        public int leadqualitycode { get; set; }
        public string _transactioncurrencyid_value { get; set; }
        public string subject { get; set; }
        public int address1_addresstypecode { get; set; }
        public bool donotbulkemail { get; set; }
        public string _modifiedby_value { get; set; }
        public bool followemail { get; set; }
        public string leadid { get; set; }
        public string _createdby_value { get; set; }
        public string websiteurl { get; set; }
        public string address1_city { get; set; }
        public int salesstagecode { get; set; }
        public string _msdyn_leadkpiid_value { get; set; }
        public double? revenue { get; set; }
        public int? purchasetimeframe { get; set; }
        public bool participatesinworkflow { get; set; }
        public int statecode { get; set; }
        public string _owningbusinessunit_value { get; set; }
        public string address2_addressid { get; set; }
        public string address1_postalcode { get; set; }
        public object telephone3 { get; set; }
        public object businesscardattributes { get; set; }
        public object address1_upszone { get; set; }
        public object address2_city { get; set; }
        public object _slainvokedid_value { get; set; }
        public object address1_postofficebox { get; set; }
        public object importsequencenumber { get; set; }
        public object utcconversiontimezonecode { get; set; }
        public object schedulefollowup_qualify { get; set; }
        public object overriddencreatedon { get; set; }
        public object stageid { get; set; }
        public object msdyn_leadscore { get; set; }
        public object address1_latitude { get; set; }
        public object address1_utcoffset { get; set; }
        public object yomifirstname { get; set; }
        public object estimatedclosedate { get; set; }
        public object _masterid_value { get; set; }
        public object lastonholdtime { get; set; }
        public object address2_fax { get; set; }
        public object address2_line1 { get; set; }
        public object address1_telephone3 { get; set; }
        public object address1_telephone2 { get; set; }
        public object address1_telephone1 { get; set; }
        public object address2_postofficebox { get; set; }
        public object emailaddress2 { get; set; }
        public object address2_latitude { get; set; }
        public string processid { get; set; }
        public object emailaddress3 { get; set; }
        public object address2_composite { get; set; }
        public object salesstage { get; set; }
        public object traversedpath { get; set; }
        public object qualificationcomments { get; set; }
        public object address2_line2 { get; set; }
        public object teamsfollowed { get; set; }
        public double? budgetamount { get; set; }
        public object address2_stateorprovince { get; set; }
        public object address2_postalcode { get; set; }
        public object estimatedamount { get; set; }
        public object entityimage_url { get; set; }
        public object initialcommunication { get; set; }
        public object msdyn_scorehistory { get; set; }
        public object timezoneruleversionnumber { get; set; }
        public object estimatedamount_base { get; set; }
        public object need { get; set; }
        public object address2_telephone3 { get; set; }
        public object address2_telephone2 { get; set; }
        public object address2_telephone1 { get; set; }
        public object address2_upszone { get; set; }
        public object _owningteam_value { get; set; }
        public object budgetstatus { get; set; }
        public object address2_line3 { get; set; }
        public object timespentbymeonemailandmeetings { get; set; }
        public object businesscard { get; set; }
        public object address2_longitude { get; set; }
        public object _modifiedonbehalfby_value { get; set; }
        public object address1_line2 { get; set; }
        public object address1_county { get; set; }
        public object schedulefollowup_prospect { get; set; }
        public object msdyn_leadscoretrend { get; set; }
        public object address1_fax { get; set; }
        public object _createdonbehalfby_value { get; set; }
        public object _accountid_value { get; set; }
        public object address2_name { get; set; }
        public object msdyn_leadgrade { get; set; }
        public object msdyn_scorereasons { get; set; }
        public object address2_utcoffset { get; set; }
        public object _campaignid_value { get; set; }
        public object _slaid_value { get; set; }
        public object fax { get; set; }
        public object address2_county { get; set; }
        public string _qualifyingopportunityid_value { get; set; }
        public object msdyn_salesassignmentresult { get; set; }
        public object address1_line3 { get; set; }
        public string _parentcontactid_value { get; set; }
        public object industrycode { get; set; }
        public int? purchaseprocess { get; set; }
        public object onholdtime { get; set; }
        public object entityimage_timestamp { get; set; }
        public object _customerid_value { get; set; }
        public object entityimageid { get; set; }
        public object lastusedincampaign { get; set; }
        public object _msdyn_segmentid_value { get; set; }
        public object _originatingcaseid_value { get; set; }
        public object telephone2 { get; set; }
        public object yomilastname { get; set; }
        public object description { get; set; }
        public object _relatedobjectid_value { get; set; }
        public object _contactid_value { get; set; }
        public object yomimiddlename { get; set; }
        public double? budgetamount_base { get; set; }
        public object address1_name { get; set; }
        public object yomicompanyname { get; set; }
        public object address1_longitude { get; set; }
        public object entityimage { get; set; }
        public object middlename { get; set; }
        public object estimatedvalue { get; set; }
        public object salutation { get; set; }
        public object pager { get; set; }
        public object address2_country { get; set; }
    }

    public class LeadsResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<Value> value { get; set; }
    }


}
