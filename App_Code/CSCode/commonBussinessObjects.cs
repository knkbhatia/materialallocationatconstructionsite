using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Oracle.DataAccess;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using OracleDataAccess;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text;

namespace BussinessObjects
{
    /// <summary>
    /// Summary description for commonBussinessObjects
    /// </summary>
    public class CreateBussinessObjects
    {
        public string ErrCode;
        //private string strSql;
        public CreateOracleDataAccessObjects dataObj;
        private OracleConnection objConnection;

        public CreateBussinessObjects() //constructor
        {
            dataObj = new CreateOracleDataAccessObjects();
        }

        //CREATED BY GAUTAM CHAUDHARY

        public DropDownList PouplateJobCodeForAllRoles(DropDownList ddlistGen, string strEmpNo, string strMainRole)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select JOBNO,JOBNO||'--'||JOB_DESC job_desc from JOB_DETAIL WHERE JOBNO is not null ");
            if (strMainRole == "USER")
            {
                strSql.Append(" and JOBNO in (select distinct JOBNO from WEB_USER where empno='");
                strSql.Append(strEmpNo);
                strSql.Append("') ");
            }
            else
            {
                strSql.Append(" ");
            }
            strSql.Append(" ORDER BY JOBNO");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "JOBNO", "JOBNO", "", "--Select Job Code--");
            return null;
        }
        public DropDownList PouplateUnitNoForAllRoles(DropDownList ddlistGen, string strJobCode, string strEmpNo, string strMainRole)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select UNIT,UNIT||'--'||UNIT_DESC UNIT_DESC from UNIT_DETAIL WHERE JOBNO='");
            strSql.Append(strJobCode);
            strSql.Append("' ");
            if (strMainRole == "USER")
            {
                strSql.Append(" and (JOBNO, UNIT) in (select JOBNO, UNIT from WEB_USER where empno='");
                strSql.Append(strEmpNo);
                strSql.Append("') ");
            }
            else
            {
                strSql.Append(" ");
            }
            strSql.Append(" ORDER BY UNIT");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "UNIT", "UNIT", "", "--Select Unit No--");
            return null;
        }
        public DropDownList PouplateInstDuctSize(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select DUCT_SIZE from EDC_DIR_INST_DUCT_WEIGHT ORDER BY DUCT_SIZE");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "DUCT_SIZE", "DUCT_SIZE", "", "--Select Duct Size--");
            return null;
        }
        public DropDownList PouplateStrlHeight(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select distinct HEIGHT from EDC_DIR_T_HT_STRL_FACTOR ORDER BY HEIGHT");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "HEIGHT", "HEIGHT", "", "--Select Strl Height--");
            return null;
        }
        public DropDownList PouplateStrlClass(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select distinct CLASS from EDC_DIR_T_HT_STRL_FACTOR ORDER BY CLASS");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "CLASS", "CLASS", "", "--Select Strl Class--");
            return null;
        }
        public DropDownList PouplateLoadCategory(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select LOAD_CATEGORY, PLANT_DESC from EDC_DIR_PLANT_CATEGORIES ORDER BY LOAD_CATEGORY");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "LOAD_CATEGORY", "PLANT_DESC", "", "--Select Load Category--");
            return null;
        }
        public DropDownList PouplateOptimaPipeRack(DropDownList ddlistGen, string strJobCode, string strUnitNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select TENDERNO||'--'||RACKID RACKID from RACK_DETAILS WHERE JOBNO='");
            strSql.Append(strJobCode);
            strSql.Append("' and UNIT='");
            strSql.Append(strUnitNo);
            strSql.Append("' ORDER BY TENDERNO, RACKID");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "RACKID", "RACKID", "", "--Select Rack Id--");
            return null;
        }
        public string GeneratePipeRack(string strJobCode, string strUnitNo, string strRackId, string strEmpNo)
        {
            OracleParameter p1 = new OracleParameter("strJobNo", strJobCode);
            OracleParameter p2 = new OracleParameter("strUnitNo", strUnitNo);
            OracleParameter p3 = new OracleParameter("strRackId", strRackId);
            OracleParameter p7 = new OracleParameter("strEmpNo", strEmpNo);
            OracleParameter p8 = new OracleParameter("P_FLAG", OracleDbType.Varchar2, 1000);
            p8.Direction = ParameterDirection.Output;

            OracleParameter[] parameters = new OracleParameter[] { p1, p2, p3, p7, p8 };
            dataObj.ExcecuteStoredProcedure("EDC_GENERATE_PIPERACK", parameters);
            string flag = p8.Value.ToString();
            return flag;
        }
        public DropDownList PouplatePipeRackId(DropDownList ddlistGen, string strJobCode, string strUnitNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select RACK_ID from EDC_PIPERACK_DTL WHERE JOBNO='");
            strSql.Append(strJobCode);
            strSql.Append("' and UNITNO='");
            strSql.Append(strUnitNo);
            strSql.Append("' ORDER BY RACK_ID");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "RACK_ID", "RACK_ID", "", "--Select Rack Id--");
            return null;
        }
        public DropDownList PouplatePortalId(DropDownList ddlistGen, string strJobCode, string strUnitNo, string strPipeRackId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select PORTAL_ID from EDC_PIPERACK_PORTAL WHERE JOBNO='");
            strSql.Append(strJobCode);
            strSql.Append("' and UNITNO='");
            strSql.Append(strUnitNo);
            strSql.Append("' and RACK_ID='");
            strSql.Append(strPipeRackId);
            strSql.Append("' ORDER BY to_number(substr(PORTAL_ID,2))");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "PORTAL_ID", "PORTAL_ID", "", "--Select Portal Id--");
            return null;
        }
        public DropDownList PouplateBayId(DropDownList ddlistGen, string strJobCode, string strUnitNo, string strPipeRackId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select BAY_ID from EDC_PIPERACK_BAY_SPACING_DTL WHERE JOBNO='");
            strSql.Append(strJobCode);
            strSql.Append("' and UNITNO='");
            strSql.Append(strUnitNo);
            strSql.Append("' and RACK_ID='");
            strSql.Append(strPipeRackId);
            strSql.Append("' ORDER BY to_number(substr(BAY_ID,2))");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "BAY_ID", "BAY_ID", "", "--Select Bay Id--");
            return null;
        }
        public DropDownList PouplateStdTierSize(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select SECTION_SIZE from STEELSECTIONS ORDER BY SECTION_TYPE, SECTION_SIZE");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "SECTION_SIZE", "SECTION_SIZE", "", "--Select Size--");
            return null;
        }
        public DropDownList PouplateStdPipeOD(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select distinct PIPE_OD from EDC_DIR_PIPE_OD ORDER BY ORDER_BY");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "PIPE_OD", "PIPE_OD", "", "--Select Pipe O.D.--");
            return null;
        }
        public DropDownList PouplateStdPipeThk(DropDownList ddlistGen, string strPipeOD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select distinct PIPE_THK from EDC_DIR_PIPE_STD where PIPE_OD='" + strPipeOD + "' ORDER BY PIPE_THK");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "PIPE_THK", "PIPE_THK", "", "--Select Pipe Thk--");
            return null;
        }
        /// <summary>
        /// Delete data from vertical member detail table on data deletion from tier detail table.
        /// </summary>
        /// <param name="strJobCode"></param>
        /// <param name="strUnitNo"></param>
        /// <param name="strRackId"></param>
        /// <param name="strPortalId"></param>
        /// <param name="strEmpNo"></param>
        /// <returns></returns>
        public string UpdateVertMemOnTierDelete(string strJobCode, string strUnitNo, string strRackId, string strPortalId, string strEmpNo)
        {
            OracleParameter p1 = new OracleParameter("strJobNo", strJobCode);
            OracleParameter p2 = new OracleParameter("strUnitNo", strUnitNo);
            OracleParameter p3 = new OracleParameter("strRackId", strRackId);
            OracleParameter p4 = new OracleParameter("strPortalId", strPortalId);
            OracleParameter p7 = new OracleParameter("strEmpNo", strEmpNo);
            OracleParameter p8 = new OracleParameter("P_FLAG", OracleDbType.Varchar2, 1000);
            p8.Direction = ParameterDirection.Output;

            OracleParameter[] parameters = new OracleParameter[] { p1, p2, p3, p4, p7, p8 };
            dataObj.ExcecuteStoredProcedure("EDC_DELETE_TIER", parameters);
            string flag = p8.Value.ToString();
            return flag;
        }
        /// <summary>
        /// Update standard data of tier and vertical members into pipe rack detail tables
        /// </summary>
        /// <param name="strJobCode"></param>
        /// <param name="strUnitNo"></param>
        /// <param name="strRackId"></param>
        /// <param name="strEmpNo"></param>
        /// <returns></returns>
        public string UpdateStdDataForPipeRAck(string strJobCode, string strUnitNo, string strRackId, string strEmpNo)
        {
            OracleParameter p1 = new OracleParameter("strJobNo", strJobCode);
            OracleParameter p2 = new OracleParameter("strUnitNo", strUnitNo);
            OracleParameter p3 = new OracleParameter("strRackId", strRackId);
            OracleParameter p7 = new OracleParameter("strEmpNo", strEmpNo);
            OracleParameter p8 = new OracleParameter("P_FLAG", OracleDbType.Varchar2, 1000);
            p8.Direction = ParameterDirection.Output;

            OracleParameter[] parameters = new OracleParameter[] { p1, p2, p3, p7, p8 };
            dataObj.ExcecuteStoredProcedure("EDC_COPY_STD_DATA", parameters);
            string flag = p8.Value.ToString();
            return flag;
        }

        public string UpdateLengthTierDtl(string strJobCode, string strUnitNo, string strRackId, string strEmpNo)
        {
            OracleParameter p1 = new OracleParameter("strJobNo", strJobCode);
            OracleParameter p2 = new OracleParameter("strUnitNo", strUnitNo);
            OracleParameter p3 = new OracleParameter("strRackId", strRackId);
            OracleParameter p7 = new OracleParameter("strEmpNo", strEmpNo);
            OracleParameter p8 = new OracleParameter("P_FLAG", OracleDbType.Varchar2, 1000);
            p8.Direction = ParameterDirection.Output;

            OracleParameter[] parameters = new OracleParameter[] { p1, p2, p3, p7, p8 };
            dataObj.ExcecuteStoredProcedure("EDC_UPDATE_TIER_LENGTH", parameters);
            string flag = p8.Value.ToString();
            return flag;
        }
        public string CalculatePipeRack(string strJobCode, string strUnitNo, string strRackId, string strEmpNo)
        {
            OracleParameter p1 = new OracleParameter("strJobNo", strJobCode);
            OracleParameter p2 = new OracleParameter("strUnitNo", strUnitNo);
            OracleParameter p3 = new OracleParameter("strRackId", strRackId);
            OracleParameter p7 = new OracleParameter("strEmpNo", strEmpNo);
            OracleParameter p8 = new OracleParameter("P_FLAG", OracleDbType.Varchar2, 1000);
            p8.Direction = ParameterDirection.Output;

            OracleParameter[] parameters = new OracleParameter[] { p1, p2, p3, p7, p8 };
            dataObj.ExcecuteStoredProcedure("EDC_CAL_PIPERACK", parameters);
            string flag = p8.Value.ToString();
            return flag;
        }



































































        /// <summary>
        /// POPULATE LOGIN ROLES
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <returns></returns>
        public DropDownList PouplateLoginRoles(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select role_id,role_desc from login_roles ORDER BY order_no");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "role_id", "role_desc", "", "--Select Role--");

            Int32 rCount = dataObj.ExecuteStatementCount("select count(*) from login_roles where role_id='USER'");
            if (rCount == 1)
            {
                ddlistGen.SelectedValue = "USER";
            }
            else
            {
                ddlistGen.SelectedValue = "";
            }

            return null;
        }

        /// <summary>
        /// POPULATE JOB CODES FROM TEMP_JOB_DTL VIEW PROVIDED BY MARKETTING
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateJobCodeFromMkt(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select job_code from temp_job_dtl WHERE JOB_CODE IS NOT NULL ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY job_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "job_code", "job_code", "", "--Select Job Code--");
            return null;
        }

        /// <summary>
        /// POPULATE CLIENT GROUP CODES FROM MKT_GRCLT SYNONIM PROVIDED BY MARKETTING
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <returns></returns>
        public DropDownList PouplateClinetGRCodeFromMkt(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;
            strSql.Append("select grclt_cd,grclt_name from mkt_grclt ");
            strSql.Append(" order by grclt_cd");

            dataObj.populate_list(ddlistGen, strSql.ToString(), "grclt_name", "grclt_cd", "", "--Select Client--");
            return null;
        }

        /// <summary>
        /// POPULATE OPEN JOB CODES FROM JOB_DETAILS TABLE OF EPS
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <returns></returns>
        public DropDownList PouplateOpenJobCodesFromJobDetails(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select job_code from job_details WHERE JOB_CODE IS NOT NULL and job_status='N' ");
            strSql.Append(" ORDER BY job_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "job_code", "job_code", "", "--Select Job Code--");
            return null;
        }

        /// <summary>
        /// POPULATE ALL JOB CODES FROM JOB_DETAILS TABLE OF EPS
        /// </summary>
        /// <param name="ddlistGen">DROPDOWN CONTROL</param>
        /// <returns></returns>
        public DropDownList PouplateAllJobCodesFromJobDetails(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select job_code from job_details WHERE JOB_CODE IS NOT NULL ");
            strSql.Append(" ORDER BY job_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "job_code", "job_code", "", "--Select Job Code--");
            return null;
        }

        /// <summary>
        /// POPULATE OPEN JOB CODES FROM EPS JOB SUPERVISOR DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWN CONTROL</param>
        /// <param name="strEmpNo">JOB SUPERVISOR EMPLOYEE CODE</param>
        /// <returns></returns>
        public DropDownList PouplateJobCodeForJobSupervisor(DropDownList ddlistGen, string strEmpNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.job_code,a.job_code||'--'||b.CLIENT_NAME job_desc from job_sup_dir a,job_details b WHERE a.job_code=b.job_code(+) and a.emp_no='");
            strSql.Append(strEmpNo);
            strSql.Append("' and b.job_status='N' ORDER BY a.job_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "job_code", "job_code", "", "--Select Job Code--");
            return null;
        }

        /// <summary>
        /// POPULATE JOB CODES FROM ALL LOGIN ROLES 
        /// </summary>
        /// <param name="ddlistGen">DROPDOWN CONTROL</param>
        /// <param name="strEmpNo">LOGGED IN EMPLOYEE CODE</param>
        /// <param name="strMainRole">LOGGED IN EMPLOYEE ROLE</param>
        /// <returns></returns>


        /// <summary>
        /// RETURN ALL THE JOB CODES FOR A PARTICULAR EPS USER
        /// </summary>
        /// <param name="strEmpNo">LOGGED IN EMPLOYEE CODE</param>
        /// <param name="strMainRole">LOGGED IN EMPLOYEE ROLE</param>
        /// <param name="strJobStatus">JOB STATUS (OPEN OR CLOSED)</param>
        /// <returns></returns>
        public string PassJobCodeForAllRoles(string strEmpNo, string strMainRole, string strJobStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select job_code from job_details WHERE job_code is not null ");
            if (strMainRole == "USER")
            {
                strSql.Append(" and job_code in (select job_code from vw_jobteam where emp_no='");
                strSql.Append(strEmpNo);
                strSql.Append("') ");
            }
            else
            {
                strSql.Append(" ");
            }

            if (strJobStatus == "OPEN")
            {
                strSql.Append(" and job_status='N'");
            }
            else
            {
                strSql.Append(" ");
            }
            //strSql.Append(" ORDER BY job_code");

            return strSql.ToString();
        }

        /// <summary>
        /// AUTHENTICATE EMPLOYEE AS JOB SUPERVISOR FOR A PARTICULAR JOB CODE
        /// </summary>
        /// <param name="strEmpNo">LOGGED IN EMPLOYEE CODE</param>
        /// <param name="strJobNo">JOB CODE</param>
        /// <param name="strMainRole">LOGGED IN EMPLOYEE ROLE</param>
        /// <returns></returns>
        public bool AuthenticateJobSup(string strEmpNo, string strJobNo, string strMainRole)
        {
            if (strMainRole == "DBA")
            {
                return true;
            }
            else if (strMainRole == "USER")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(*) from job_sup_dir WHERE emp_no='");
                strSql.Append(strEmpNo);
                strSql.Append("' and job_code ='");
                strSql.Append(strJobNo);
                strSql.Append("'");
                Int32 rCount = dataObj.ExecuteStatementCount(strSql.ToString());
                if (rCount == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// POPULATE UNIT CODES FROM VIEW PROJUNITS_VW PROVIDED BY PPMS
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strJobCode">JOB CODE</param>
        /// <returns></returns>

        public DropDownList PouplateUnitCodesFromPPMS(DropDownList ddlistGen, string strJobCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nvl(unitcode,'000') unitcode from projunits_vw WHERE projno ='");
            strSql.Append(strJobCode.ToString());
            strSql.Append("' ORDER BY unitcode");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "unitcode", "unitcode", "", "--Select Unit No--");
            return null;
        }

        /// <summary>
        /// POPULATE DIVISION FROM VIEW VW_DIVDEPT OF CPIS DIV-DEPT DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateDivFromCPIS(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct divn_desc,divn_code from VW_DIVDEPT WHERE divn_code IS NOT NULL ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY divn_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "divn_code", "divn_desc", "", "--Select Div--");
            return null;
        }

        /// <summary>
        /// POPULATE DEPARTMENTS FROM VIEW VW_DIVDEPT OF CPIS DIV-DEPT DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateDeptFromCPIS(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct DEPT_DESC,DEPT_CODE from VW_DIVDEPT where DEPT_CODE IS NOT NULL ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY DEPT_DESC");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "DEPT_CODE", "DEPT_DESC", "", "--Select Dept--");
            return null;
        }

        /// <summary>
        /// POPULATE CURRENTLY SEARVING EMPLOYEES FROM VIEW VW_EMPLOYEE OF CPIS EMPLOYEE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateEmpFromCPIS(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct empname empdesc,empno from vw_employee where empno IS NOT NULL and sep_type='0' ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY empname");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "empno", "empdesc", "", "--Select Employee--");
            return null;
        }

        /// <summary>
        /// POPULATE JOB TYPES FROM VIEW FROM JOB TYPE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateJobType(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select job_type_code,job_type_desc from job_type_dir WHERE job_type_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY job_type_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "job_type_code", "job_type_desc", "", "--Select Job Type--");
            return null;
        }

        /// <summary>
        /// POPULATE COUNTRIES FROM EPS COUNTRY DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateCountry(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select country_code_3,country_name from country_dir WHERE country_code_3 IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY country_name");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "country_code_3", "country_name", "", "--Select Country--");
            ddlistGen.SelectedValue = "IND";
            return null;
        }

        /// <summary>
        /// POPULATE ACTOR TYPE'S FROM EPS ACTOR TYPE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateActorType(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select actor_type,actor_desc from actor_dir WHERE actor_type IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY actor_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "actor_type", "actor_desc", "", "--Select Actor Type--");
            return null;
        }

        /// <summary>
        /// POPULATE DOCUMENT TYPE'S FROM EPS DOCUMENT TYPE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateDocType(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select doc_type_code,doc_name from document_type_dir WHERE doc_type_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY doc_name");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "doc_type_code", "doc_name", "", "--Select Doc Type--");
            return null;
        }

        /// <summary>
        /// POPULATE ADDRESS TYPE'S FROM EPS ADDRESS TYPE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateAddType(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct add_type_code,add_type_desc from address_type_dir WHERE add_type_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY add_type_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "add_type_code", "add_type_desc", "", "--Select Add Type--");
            return null;
        }

        /// <summary>
        /// POPULATE ADDRESS CODE'S FROM EPS EIL ADDRESS CODE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateEilAddCode(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select address_code,short_name from eil_address_dir WHERE address_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY address_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "address_code", "short_name", "", "--Select Address--");
            return null;
        }

        /// <summary>
        /// POPULATE JOB CLIENT ADDRESS CODE'S FROM EPS JOB CLIENT ADDRESS CODE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateJobClientAddCode(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select address_code,short_name from job_client_address where address_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY address_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "address_code", "short_name", "", "--Select Address--");
            return null;
        }

        /// <summary>
        /// POPULATE CURRENCY CODE'S FROM EPS CURRENCY DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateCurrency(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select currency_code,currency_name from currency_dir where currency_code IS NOT NULL ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY currency_name");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "currency_code", "currency_name", "", "--Select Currency--");
            ddlistGen.SelectedValue = "INR";
            return null;
        }

        /// <summary>
        /// POPULATE REGION CODE'S FROM PDD REGION DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateRegionFromPDD(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select rpo_name,rpo_pdd_code from region_dir where ho_rpo='HO' and rpo_pdd_code IS NOT NULL ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY rpo_name");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "rpo_pdd_code", "rpo_name", "", "--Select Region--");
            return null;
        }

        /// <summary>
        /// POPULATE ALL THE EPS USER NAMES FROM EPS USER DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strEmpCode">SUPERVISOR EMPLOYEE CODE</param>
        /// <param name="strRPOCode">REGION CODE</param>
        /// <returns></returns>
        public DropDownList PouplateEPSUser(DropDownList ddlistGen, string strEmpCode, string strRPOCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select emp_no,empname from (select a.emp_no,b.empname from eps_user_dir a,vw_employee b where a.emp_no=b.empno(+) and a.lock_flag='N' ");
            if (strRPOCode.Length != 0)
            {
                strSql.Append(" and a.rpo_pdd_code ='");
                strSql.Append(strRPOCode);
                strSql.Append("' ");
            }
            if (strEmpCode.Length != 0)
            {
                strSql.Append(" start with a.sup_emp_no ='");
                strSql.Append(strEmpCode);
                strSql.Append("' connect by prior a.emp_no = a.sup_emp_no union select empno,empname from vw_employee where empno='");
                strSql.Append(strEmpCode);
                strSql.Append("' ");
            }
            strSql.Append(") ORDER BY empname");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "emp_no", "empname", "", "--Select EPS User--");
            return null;
        }

        /// <summary>
        /// POPULATE EPS USER NAMES FOR A PARTICULAR JOB CODE FROM JOB USER DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strEmpCode">SUPERVISOR EMPLOYEE CODE</param>
        /// <param name="strRPOCode">REGION CODE</param>
        /// <param name="strJobCode">JOB CODE</param>
        /// <returns></returns>
        public DropDownList PouplateEPSUserForJob(DropDownList ddlistGen, string strEmpCode, string strRPOCode, string strJobCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select emp_no,empname from (select a.emp_no,b.empname from job_user_list a,vw_employee b,eps_user_dir c where a.emp_no=b.empno(+) and a.emp_no=c.emp_no(+) and a.job_code='");
            strSql.Append(strJobCode);
            strSql.Append("' and c.lock_flag='N' ");
            if (strRPOCode.Length != 0)
            {
                strSql.Append(" and c.rpo_pdd_code ='");
                strSql.Append(strRPOCode);
                strSql.Append("' ");
            }
            if (strEmpCode.Length != 0)
            {
                strSql.Append(" start with a.sup_emp_no ='");
                strSql.Append(strEmpCode);
                strSql.Append("' connect by prior a.emp_no = a.sup_emp_no union select empno,empname from vw_employee where empno='");
                strSql.Append(strEmpCode);
                strSql.Append("'  ");
            }

            strSql.Append(" union select EMP_NO,EIL_EMP_NAME(EMP_NO) empname from JOB_SUP_DIR where JOB_CODE='");
            strSql.Append(strJobCode);
            strSql.Append("' ");

            strSql.Append(") ORDER BY empname");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "emp_no", "empname", "", "--Select EPS User--");
            return null;
        }

        /// <summary>
        /// POPULATE ROLE ID FROM ROLE ID DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateRoleID(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select role_id,role_desc from role_dir where role_id IS NOT NULL ");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY role_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "role_id", "role_desc", "", "--Select Role--");
            return null;
        }

        /// <summary>
        /// POPULATE PURCHASE TYPE FROM PURCHASE TYPE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplatePurchaseType(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct it_code,it_desc from item_type_cpms where it_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY it_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "it_code", "it_desc", "", "--Select Purchase Type--");
            return null;
        }

        /// <summary>
        /// POPULATE JOB BID RULE DEPARTMENT FROM JOB BID RULE DEPARTMENT DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateJobBidRuleDept(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select srno,dept_name from job_bid_rule_dept where srno IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY dept_name");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "srno", "dept_name", "", "--Select Department--");
            return null;
        }

        /// <summary>
        /// POPULATE CQ CLAUSE FROM CQ CLAUSE DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateCQClause(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cq_clause_code,cq_clause_desc from cq_clause_dir where cq_clause_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY cq_clause_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "cq_clause_code", "cq_clause_desc", "", "--Select CQ Clause--");
            return null;
        }

        /// <summary>
        /// POPULATE JOB CODES FOR A PARTICULAR USER
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strEmpNo">EMPLOYEE CODE</param>
        /// <param name="strRoleId">ROLE ID</param>
        /// <returns></returns>
        public DropDownList PouplateJobForUser(DropDownList ddlistGen, string strEmpNo, string strRoleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select job_code from job_user_list where emp_no='");
            strSql.Append(strEmpNo);
            strSql.Append("' and role_id='");
            strSql.Append(strRoleId);
            strSql.Append("' order by job_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "job_code", "job_code", "", "--Select Job Code--");
            return null;
        }

        /// <summary>
        /// POPULATE ITEM CODES FROM PDD ITEM DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">WHERE CONDITION</param>
        /// <returns></returns>
        public DropDownList PouplateItemCodesFromPDD(DropDownList ddlistGen, string strWhereCond)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select item_code,item_code||'-'||item_desc item_desc from PDD_ITEM_DIR where item_code IS NOT NULL");
            strSql.Append(strWhereCond);
            strSql.Append(" ORDER BY item_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "item_code", "item_desc", "", "--Select Item Code--");
            return null;
        }

        /// <summary>
        /// POPULATE REVISION REASONS FROM REVISION REASONS DIRECTORY
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strWhereCond">Vendor Change Type. A-Addition, D-Deletion</param>
        /// <returns></returns>
        public DropDownList PouplateRevisionReasons(DropDownList ddlistGen, string strVendorChangeType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select reason_vc_code,reason_vc_desc from reason_vend_change where reason_vc_code IS NOT NULL and VEND_CHANGE_TYPE='");
            strSql.Append(strVendorChangeType);
            strSql.Append("' ORDER BY reason_vc_desc");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "reason_vc_code", "reason_vc_desc", "", "--Select Reason--");
            return null;
        }


        /// <summary>
        /// BIND SUPPLIER-ITEMCODE CAPABILITY
        /// </summary>
        /// <param name="gvGen">GRIDVIEW CONTROL</param>
        /// <param name="strSuppCode">SUPPLIER CODE</param>
        /// <param name="strItemCode">ITEM CODE</param>
        /// <returns></returns>
        public GridView BindCapability(GridView gvGen, string strSuppCode, string strItemCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select status,parameter,group_code,range_from,range_to,uom,ind_imp,ap,remarks,sel_flg,pcode from pdd_vendor_item_dir where vendor_code='");
            strSql.Append(strSuppCode);
            strSql.Append("' and item_code='");
            strSql.Append(strItemCode);
            strSql.Append("' order by pcode");
            dataObj.BindGridView(gvGen, strSql.ToString());
            return null;
        }

        /// <summary>
        /// BIND SUPPLIER-ITEMCODE HOLIDAY
        /// </summary>
        /// <param name="gvGen">GRIDVIEW CONTROL</param>
        /// <param name="strSuppCode">SUPPLIER CODE</param>
        /// <param name="strItemCode">ITEM CODE</param>
        /// <returns></returns>
        public GridView BindHoliday(GridView gvGen, string strSuppCode, string strItemCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.constraint_code,b.cons_desc,to_char(a.constraint_from,'dd-MON-yyyy') constraint_from,to_char(a.constraint_to,'dd-MON-yyyy') constraint_to from pdd_vendor_item_cons_dir a,pdd_constraint_dir b where a.constraint_code=b.cons_code(+) and a.vendor_code='");
            strSql.Append(strSuppCode);
            strSql.Append("' and a.item_code='");
            strSql.Append(strItemCode);
            strSql.Append("' order by a.constraint_code");
            dataObj.BindGridView(gvGen, strSql.ToString());
            return null;
        }

        /// <summary>
        /// This will bind supplier holiday constraints for all the items.
        /// </summary>
        /// <param name="gvGen"></param>
        /// <param name="strSuppCode"></param>
        /// <param name="strItemCode"></param>
        /// <returns></returns>
        public GridView BindHolidayForSuppOnly(GridView gvGen, string strSuppCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.item_code,a.constraint_code,b.cons_desc,to_char(a.constraint_from,'dd-MON-yyyy') constraint_from,to_char(a.constraint_to,'dd-MON-yyyy') constraint_to from pdd_vendor_item_cons_dir a,pdd_constraint_dir b where a.constraint_code=b.cons_code(+) and a.vendor_code='");
            strSql.Append(strSuppCode);
            strSql.Append("' order by a.item_code,a.constraint_code");
            dataObj.BindGridView(gvGen, strSql.ToString());
            return null;
        }
        /// <summary>
        /// POPULATE PROJECT SUPPLIER REVISION NO'S FROM PSL HDR
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strJobCode">JOB CODE</param>
        /// <returns></returns>
        public DropDownList PouplatePSLRevNos(DropDownList ddlistGen, string strJobCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psl_rev_no from job_sup_list_hdr where job_code='");
            strSql.Append(strJobCode);
            strSql.Append("' ORDER BY psl_rev_no");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "psl_rev_no", "psl_rev_no", "", "--Select PSL Rev No.--");
            return null;
        }

        /// <summary>
        /// BIND ITEMCODE FOR A PSL
        /// </summary>
        /// <param name="gvGen">GRIDVIEW CONTROL</param>
        /// <param name="strJobCode">JOB CODE</param>
        /// <param name="strPSLRevNo">PSL REV NO</param>
        /// <returns></returns>
        public GridView BindItemsForPSL(GridView gvGen, string strJobCode, string strPSLRevNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct a.item_code,b.item_desc from job_sup_list_iw a,pdd_item_dir b where a.item_code=b.item_code(+) and a.job_code='");
            strSql.Append(strJobCode);
            strSql.Append("' and a.psl_rev_no='");
            strSql.Append(strPSLRevNo);
            strSql.Append("' order by a.item_code");
            dataObj.BindGridView(gvGen, strSql.ToString());
            return null;
        }

        /// <summary>
        /// BIND SUPPLIER'S FOR A ITEMCODE FOR A PSL
        /// </summary>
        /// <param name="gvGen">GRIDVIEW CONTROL</param>
        /// <param name="strJobCode">JOB CODE</param>
        /// <param name="strPSLRevNo">PSL REV NO</param>
        /// <param name="strItemCode">ITEM CODE</param>
        /// <returns></returns>
        public GridView BindSuppForItemInPSL(GridView gvGen, string strJobCode, string strPSLRevNo, string strItemCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;
            strSql.Append("select a.JOB_CODE,a.psl_rev_no,a.vendor_code,b.vendor_name,b.ind_imp,a.holiday_flag,a.remarks,a.remove_flag,a.reason_code,a.AUTO_MANUAL,case a.AUTO_MANUAL when 'A' then 'Auto' when 'M' then 'Manual' end AUTO_MANUAL_desc,a.supplier_type,case a.supplier_type when 'P' then 'Potential' when 'T' then 'Temporary' end supplier_type_desc,c.REASON_VC_DESC reason_desc ");
            strSql.Append("from job_sup_list_iw a,pdd_vendor_dir b,REASON_VEND_CHANGE c where a.vendor_code=b.vendor_code(+) and a.reason_code=c.REASON_VC_CODE(+) and a.JOB_CODE='");
            strSql.Append(strJobCode.ToString());
            strSql.Append("' and a.psl_rev_no ='");
            strSql.Append(strPSLRevNo);
            strSql.Append("' and a.item_code ='");
            strSql.Append(strItemCode.ToString());
            strSql.Append("' order by a.vendor_code");

            dataObj.BindGridView(gvGen, strSql.ToString());
            return null;
        }

        /// <summary>
        /// POPULATE BID TYPE
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <returns></returns>
        public DropDownList PouplateItemTypeGroup(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ITEM_TYPE_GR_CODE, ITEM_TYPE_GR_DESC FROM ITEM_TYPE_GROUP ORDER BY ITEM_TYPE_GR_DESC");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "ITEM_TYPE_GR_CODE", "ITEM_TYPE_GR_DESC", "", "--Select BID Type--");
            return null;
        }

        /// <summary>
        /// POPULATE MR TYPE ON BASIS OF BID TYPE
        /// </summary>
        /// <param name="ddlistGen">DROPDOWNLIST CONTROL</param>
        /// <param name="strItemTypeGroup"></param>
        /// <returns></returns>
        public DropDownList PouplateITCode(DropDownList ddlistGen, string strItemTypeGroup)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT distinct IT_CODE, IT_DESC FROM ITEM_TYPE_CPMS WHERE IT_CODE is not null ");
            if (strItemTypeGroup.Length != 0)
            {
                strSql.Append(" and ITEM_TYPE_GR_CODE = '");
                strSql.Append(strItemTypeGroup);
                strSql.Append("' ");
            }
            //strSql.Append(" ");

            dataObj.populate_list(ddlistGen, strSql.ToString(), "IT_CODE", "IT_DESC", "", "--Select MR Type--");
            return null;
        }

        public DropDownList PouplateSignatoryType(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SIGNATORY_TYPE_CODE, SIGNATORY_DESC FROM SIGNATORY_TYPE_DIR ORDER BY SIGNATORY_DESC");

            dataObj.populate_list(ddlistGen, strSql.ToString(), "SIGNATORY_TYPE_CODE", "SIGNATORY_DESC", "", "--Select Signatory Type--");
            return null;
        }
        public DropDownList PouplateJobSignatory(DropDownList ddlistGen, string strJobCode, string strSignatoryFrom, string strSignatoryType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PERSON_CODE, PERSON_NAME FROM JOB_SIGN_LIST WHERE JOB_CODE='");
            strSql.Append(strJobCode);
            strSql.Append("' AND SIGNATORY_TYPE='");
            strSql.Append(strSignatoryFrom);
            strSql.Append("' AND SIGNATORY_TYPE_CODE='");
            strSql.Append(strSignatoryType);
            strSql.Append("' ORDER BY PERSON_NAME");

            dataObj.populate_list(ddlistGen, strSql.ToString(), "PERSON_CODE", "PERSON_NAME", "", "--Select Signatory--");
            return null;
        }
        /// <summary>
        /// Get supplier name by provide supplier code
        /// </summary>
        /// <param name="strSupplierCode">Supplier Code</param>
        /// <returns></returns>
        public string GetSupplierName(string strSupplierCode)
        {
            return dataObj.ExecuteStatementString("select VENDOR_NAME from PDD_VENDOR_DIR where VENDOR_CODE='" + strSupplierCode + "'");
        }

        /// <summary>
        /// populate Delivery basis drop down on the basis or category of vendor i.e. Indian or Foreign.
        /// </summary>
        /// <param name="ddlistGen">Dropdownlist</param>
        /// <param name="strBidderType">Bidder Type</param>
        /// <returns></returns>
        public DropDownList PouplateDeliveryBasis(DropDownList ddlistGen, string strBidderType)
        {
            StringBuilder strSql = new StringBuilder();
            //string strSQL;
            if (strBidderType == "IND")
            {
                strSql.Append("SELECT DP_BASIS_CODE, DP_BASIS_DESC FROM STD_DP_BASIS_DIR  WHERE APPL_IND = 'Y' ORDER BY DP_BASIS_DESC");
                //strSQL = "SELECT DP_BASIS_CODE, DP_BASIS_DESC FROM STD_DP_BASIS_DIR  WHERE APPL_IND = 'Y' ORDER BY DP_BASIS_DESC";
            }
            else
            {
                strSql.Append("SELECT DP_BASIS_CODE, DP_BASIS_DESC FROM STD_DP_BASIS_DIR  WHERE APPL_IMP = 'Y' ORDER BY DP_BASIS_DESC");
                //strSQL = "SELECT DP_BASIS_CODE, DP_BASIS_DESC FROM STD_DP_BASIS_DIR  WHERE APPL_IMP = 'Y' ORDER BY DP_BASIS_DESC";
            }

            dataObj.populate_list(ddlistGen, strSql.ToString(), "DP_BASIS_CODE", "DP_BASIS_DESC", "SDB", "Select Delivery Basis");
            return null;
        }

        /// <summary>
        /// Populate short names of Eil or Client address.
        /// </summary>
        /// <param name="ddlistGen">Dropdownlist</param>
        /// <param name="strJobCode">Job Code</param>
        /// <param name="strEilClient">Eil or Client</param>
        /// <returns></returns>
        public DropDownList PouplateEilClientAddress(DropDownList ddlistGen, string strJobCode, string strEilClient)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            if (strEilClient == "E")
            {
                strSql.Append("SELECT ADDRESS_CODE, SHORT_NAME FROM VW_ALL_ADDRESS WHERE ADD_OF = 'E'  and JOB_CODE='XXXX' ORDER BY SHORT_NAME");
            }
            else if (strEilClient == "C")
            {
                strSql.Append("SELECT ADDRESS_CODE, SHORT_NAME FROM VW_ALL_ADDRESS WHERE ADD_OF = 'C' and JOB_CODE='" + strJobCode.ToString() + "' ORDER BY SHORT_NAME");
            }

            dataObj.populate_list(ddlistGen, strSql.ToString(), "ADDRESS_CODE", "SHORT_NAME", "", "--Select Add--");
            return null;
        }

        /// <summary>
        /// Get complete address of Eil or Client address.
        /// </summary>
        /// <param name="strJobCode"></param>
        /// <param name="strEilClient"></param>
        /// <param name="strAddCode"></param>
        /// <returns></returns>
        public string GetEilClientAddress(string strJobCode, string strEilClient, string strAddCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            if (strEilClient == "E")
            {
                strSql.Append("SELECT ADDRESS_1||', '||ADDRESS_2||', '||ADDRESS_3||', '||CITY||', '||STATE||', '||GET_COUNTRY_NAME(COUNTRY_CODE_3)||'-'||PINCODE Address FROM VW_ALL_ADDRESS WHERE ADD_OF = 'E' and JOB_CODE='XXXX' and ADDRESS_CODE='" + strAddCode.ToString() + "'");
            }
            else if (strEilClient == "C")
            {
                strSql.Append("SELECT ADDRESS_1||', '||ADDRESS_2||', '||ADDRESS_3||', '||CITY||', '||STATE||', '||GET_COUNTRY_NAME(COUNTRY_CODE_3)||'-'||PINCODE Address FROM VW_ALL_ADDRESS WHERE ADD_OF = 'C' and JOB_CODE='" + strJobCode.ToString() + "'  and ADDRESS_CODE='" + strAddCode.ToString() + "'");
            }

            string strAddress = dataObj.ExecuteStatementString(strSql.ToString());
            return strAddress;
        }


        public string GetEilEmpDesignation(string strEmpCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("SELECT DESIGNATION FROM VW_EMPLOYEE WHERE EMPNO='" + strEmpCode.ToString() + "'");

            string strDesignation = dataObj.ExecuteStatementString(strSql.ToString());
            return strDesignation;
        }

        /// <summary>
        /// Populate checklist activities
        /// </summary>
        /// <param name="ddlistGen"></param>
        /// <returns></returns>
        public DropDownList PouplateChecklistActivity(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select ACTIVITY_CODE,ACTIVITY_DESC from CHECKLIST_ACTIVITY ");
            strSql.Append(" order by ACTIVITY_CODE");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "ACTIVITY_CODE", "ACTIVITY_DESC", "", "--Select Activity--");
            return null;
        }

        /// <summary>
        /// Populate checklist clauses
        /// </summary>
        /// <param name="ddlistGen"></param>
        /// <returns></returns>
        public DropDownList PouplateChecklistClauses(DropDownList ddlistGen)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Length = 0;

            strSql.Append("select CHK_CLAUSE_CODE,CHK_CLAUSE_DESC from CHECKLIST_CLAUSE ");
            strSql.Append(" order by CHK_CLAUSE_CODE");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "CHK_CLAUSE_CODE", "CHK_CLAUSE_DESC", "", "--Select Clause--");
            return null;
        }

        /// <summary>
        /// Populate Job TPI List
        /// </summary>
        /// <param name="ddlistGen"></param>
        /// <param name="strJobCode"></param>
        /// <returns></returns>
        public DropDownList PouplateJobTPI(DropDownList ddlistGen, string strJobCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.tpi_code,b.tpi_name from JOB_TPI_LIST a,tpi_hdr b where a.tpi_code=b.tpi_code and a.job_code='");
            strSql.Append(strJobCode);
            strSql.Append("' ORDER BY a.tpi_code");
            dataObj.populate_list(ddlistGen, strSql.ToString(), "tpi_code", "tpi_name", "", "--Select TPI--");
            return null;
        }
    }

    // return employee informations

    public class EmployeeInfo
    {
        public string strIntercom;
        public string strMobile;
        public string strEmployeeName;
        public string strEmployeeDivision;
        public string strEmployeeDepartment;
        public string strEmployeeEMail;
        public string strEmployeeIntercom;
        public string strEmployeeDesignation;
        public string strEmployeeDetails;
        public string strEmployeeMobileNo;
        private CreateOracleDataAccessObjects dataObj;
        private OracleConnection objConnection;
        private StringBuilder strSql = new StringBuilder();
        //private string errStr;
        private OracleDataReader objDataReader;

        public EmployeeInfo()          //constructor
        {
            dataObj = new CreateOracleDataAccessObjects();
        }

        public void GetUserInfoFromPDB(string strEmpNo)
        {
            OracleDataReader objDatareader;
            strSql.Length = 0;

            strEmployeeName = "";
            strEmployeeDivision = "";
            strEmployeeDepartment = "";
            strEmployeeEMail = "";
            strEmployeeIntercom = "";
            strEmployeeDesignation = "";
            strEmployeeMobileNo = "";
            strEmployeeDetails = "";

            strSql.Append("SELECT PRST_DIVN, PRST_SECTN, EMPNAME, EMAIL1, INTERCOM, DESIGNATION, ");
            strSql.Append(" EMPNAME || ' (E-Mail : ' || EMAIL1 || ', ICom : ' || INTERCOM || ')' USER_DETAIL,mobile_sms ");
            strSql.Append(" FROM vw_employee where EMPNO = '");
            strSql.Append(strEmpNo);
            strSql.Append("'");


            objConnection = dataObj.open_connection();
            objDatareader = dataObj.DataReader(strSql.ToString(), objConnection);


            if (objDatareader.HasRows)
            {
                while (objDatareader.Read())
                {
                    strEmployeeDivision = objDatareader.GetValue(0).ToString();
                    strEmployeeDepartment = objDatareader.GetValue(1).ToString();
                    strEmployeeName = objDatareader.GetValue(2).ToString();
                    strEmployeeEMail = objDatareader.GetValue(3).ToString();
                    strEmployeeIntercom = objDatareader.GetValue(4).ToString();
                    strEmployeeDesignation = objDatareader.GetValue(5).ToString();
                    strEmployeeDetails = "<a href='mailto:" + objDatareader.GetValue(3).ToString() + "'>" + objDatareader.GetValue(6).ToString() + "</a>";
                    strEmployeeMobileNo = objDatareader.GetValue(7).ToString();
                }
            }
            objDatareader.Close();
            objDatareader.Dispose();
            objConnection.Close();
            objConnection.Dispose();
        }
    }


    public class JobInfo
    {
        public string strProjName;
        public Int32 intTotalMRs;
        public string strJobStatus;
        public string strPMEmpNo;
        public string strJobType;
        public string strTQPRImpAllowed;
        public string strInsuranceParty;
        public string strClientGRCode;
        public string strClientBRCode;
        public string strClientName;
        public string strClientAddress;
        public string strClientCity;
        public string strClientState;
        public string strClientCountry;
        public string strClientPincode;

        private CreateOracleDataAccessObjects dataObj;
        private OracleConnection objConnection;
        private StringBuilder strSql = new StringBuilder();
        //private string errStr;
        private OracleDataReader objDataReader;

        public JobInfo()          //constructor
        {
            dataObj = new CreateOracleDataAccessObjects();
        }

        public void GetJobInfoFromJobDetails(string strJobCode)
        {
            OracleDataReader objDatareader;
            strSql.Length = 0;

            strProjName = "";
            intTotalMRs = 0;
            strJobStatus = "";
            strPMEmpNo = "";
            strJobType = "";
            strTQPRImpAllowed = "";
            strInsuranceParty = "";
            strClientGRCode = "";
            strClientBRCode = "";
            strClientName = "";
            strClientAddress = "";
            strClientCity = "";
            strClientState = "";
            strClientCountry = "";
            strClientPincode = "";

            strSql.Append("SELECT PROJ_NAME, TOTAL_MRS, JOB_STATUS, PROJ_HEAD_EMP_NO, JOB_TYPE_CODE, TQ_PR_IMP_ALLOW, INSURANCE_PARTY, CLIENT_GR_CODE, ");
            strSql.Append("CLIENT_BR_CODE, CLIENT_ADD1||' '||CLIENT_ADD2||' '||CLIENT_ADD3 CLIENT_ADDRESS, CLIENT_CITY, CLIENT_STATE, CLIENT_COUNTRY, ");
            strSql.Append("CLIENT_NAME, CLIENT_PINCODE ");
            strSql.Append(" FROM JOB_DETAILS where JOB_CODE = '");
            strSql.Append(strJobCode);
            strSql.Append("'");


            objConnection = dataObj.open_connection();
            objDatareader = dataObj.DataReader(strSql.ToString(), objConnection);


            if (objDatareader.HasRows)
            {
                while (objDatareader.Read())
                {
                    strProjName = Convert.ToString(objDatareader.GetValue(0));
                    intTotalMRs = Convert.ToInt32(objDatareader.GetValue(1));
                    strJobStatus = Convert.ToString(objDatareader.GetValue(2));
                    strPMEmpNo = Convert.ToString(objDatareader.GetValue(3));
                    strJobType = Convert.ToString(objDatareader.GetValue(4));
                    strTQPRImpAllowed = Convert.ToString(objDatareader.GetValue(5));
                    strInsuranceParty = Convert.ToString(objDatareader.GetValue(6));
                    strClientGRCode = Convert.ToString(objDatareader.GetValue(7));
                    strClientBRCode = Convert.ToString(objDatareader.GetValue(8));
                    strClientName = Convert.ToString(objDatareader.GetValue(13));
                    strClientAddress = Convert.ToString(objDatareader.GetValue(9));
                    strClientCity = Convert.ToString(objDatareader.GetValue(10));
                    strClientState = Convert.ToString(objDatareader.GetValue(11));
                    strClientCountry = Convert.ToString(objDatareader.GetValue(12));
                    strClientPincode = Convert.ToString(objDatareader.GetValue(14));
                }
            }
            objDatareader.Close();
            objDatareader.Dispose();
            objConnection.Close();
            objConnection.Dispose();
        }
    }

    public class MRInfo
    {
        public string strMRNo;
        public string strUnitNo;
        public string strCC;
        public string strMRSlNo;
        public string strMRRevNo;
        public Int32 intMaxAllocRevNo;
        public string strEnquiryTypeCode;
        public string strEnquiryModeCode;
        public string strEnquiryModeCodeDesc;
        public string strEnquiryBasicCode;
        public string strMRCategory;
        public string strMRDesc;
        public Int32 intOriginDiv;
        public Int32 intOriginDept;
        public string strEDMSRelDate;
        public string strProjInputRelDate;
        public string strProjInputRevNo;
        public string strMRRelDate;
        public string strMRAllocDate;
        public string strProjInputProcessingFlag;
        public string strEPSProcessingFlag;
        public string strProjInputProcessingFlagDesc;
        public string strEPSProcessingFlagDesc;
        public string strProjCoordinatorEmpNo;
        public string strEnggApprover;
        public string strEnggPerformer;
        public string strBiddingType;
        public string strCPSupervisor;
        public string strCPPerformer;
        public string strSubmissionFormat;
        public string strReverseAuction;
        public string strPkgFlag;
        public string strItemType;

        private CreateOracleDataAccessObjects dataObj;
        private OracleConnection objConnection;
        private StringBuilder strSql = new StringBuilder();
        //private string errStr;
        private OracleDataReader objDataReader;

        public MRInfo()          //constructor
        {
            dataObj = new CreateOracleDataAccessObjects();
        }

        public void GetMRInfoFromMRHeaderByRFQNo(string strJobCode, Int32 strRFQNo)
        {
            OracleDataReader objDatareader;
            strSql.Length = 0;

            strMRNo = "";
            strUnitNo = "";
            strCC = "";
            strMRSlNo = "";
            strMRRevNo = "";
            intMaxAllocRevNo = 0;
            strEnquiryTypeCode = "";
            strEnquiryModeCode = "";
            strEnquiryModeCodeDesc = "";
            strEnquiryBasicCode = "";
            strMRCategory = "";
            strMRDesc = "";
            intOriginDiv = 0;
            intOriginDept = 0;
            strEDMSRelDate = "";
            strProjInputRelDate = "";
            strProjInputRevNo = "";
            strMRRelDate = "";
            strMRAllocDate = "";
            strProjInputProcessingFlag = "";
            strEPSProcessingFlag = "";
            strProjInputProcessingFlagDesc = "";
            strEPSProcessingFlagDesc = "";
            strProjCoordinatorEmpNo = "";
            strEnggApprover = "";
            strEnggPerformer = "";
            strBiddingType = "";
            strCPSupervisor = "";
            strCPPerformer = "";
            strSubmissionFormat = "";
            strReverseAuction = "";
            strPkgFlag = "";
            strItemType = "";

            strSql.Append("SELECT JOB_CODE, UNIT, CC, MR_SL_NO, MR_REV_NO, MAX(ALLOC_REV_NO) ALLOC_REV_NO, TYPE_ENQ_CODE, ITEM_TYPE_GR_CODE, IT_CODE, MR_CATEGORY, ");
            strSql.Append("MR_DESC, ORIG_DIVISION, ORIG_DEPARTMENT, EDMS_REL_DATE, MR_INPUT_REL_DATE, INPUT_REV_NO, MR_REL_DATE, MR_ALLOC_DATE, PROJ_INPUT_PROCESS_FLAG, ");
            strSql.Append("PROJ_CO_EMP_NO, ORIG_SUP_EMP_NO, ORIG_PER_EMP_NO, ENQ_TYPE, CP_SUP_EMP_NO, CP_PER_EMP_NO, SUB_FORMAT, REV_AUCTION, EPS_PROCESS_FLAG, PKG_FLAG, ITEM_TYPE ");
            strSql.Append(" FROM MR_HEADER where JOB_CODE = '");
            strSql.Append(strJobCode);
            strSql.Append("' AND RFQ_NO='");
            strSql.Append(strRFQNo);
            strSql.Append("' GROUP BY JOB_CODE, UNIT, CC, MR_SL_NO, MR_REV_NO, TYPE_ENQ_CODE, ITEM_TYPE_GR_CODE, IT_CODE, MR_CATEGORY, ");
            strSql.Append("MR_DESC, ORIG_DIVISION, ORIG_DEPARTMENT, EDMS_REL_DATE, MR_INPUT_REL_DATE, INPUT_REV_NO, MR_REL_DATE, MR_ALLOC_DATE, PROJ_INPUT_PROCESS_FLAG, ");
            strSql.Append("PROJ_CO_EMP_NO, ORIG_SUP_EMP_NO, ORIG_PER_EMP_NO, ENQ_TYPE, CP_SUP_EMP_NO, CP_PER_EMP_NO, SUB_FORMAT, REV_AUCTION, EPS_PROCESS_FLAG, PKG_FLAG, ITEM_TYPE ");


            objConnection = dataObj.open_connection();
            objDatareader = dataObj.DataReader(strSql.ToString(), objConnection);


            if (objDatareader.HasRows)
            {
                while (objDatareader.Read())
                {
                    strUnitNo = Convert.ToString(objDatareader.GetValue(1));
                    strCC = Convert.ToString(objDatareader.GetValue(2));
                    strMRSlNo = Convert.ToString(objDatareader.GetValue(3));
                    strMRRevNo = Convert.ToString(objDatareader.GetValue(4));
                    intMaxAllocRevNo = Convert.ToInt32(objDatareader.GetValue(5));
                    strEnquiryTypeCode = Convert.ToString(objDatareader.GetValue(6));
                    strEnquiryModeCode = Convert.ToString(objDatareader.GetValue(7));
                    strEnquiryBasicCode = Convert.ToString(objDatareader.GetValue(8));
                    strMRCategory = Convert.ToString(objDatareader.GetValue(9));
                    strMRDesc = Convert.ToString(objDatareader.GetValue(10));
                    intOriginDiv = Convert.ToInt32(objDatareader.GetValue(11));
                    intOriginDept = Convert.ToInt32(objDatareader.GetValue(12));
                    strEDMSRelDate = Convert.ToString(objDatareader.GetValue(13));
                    strProjInputRelDate = Convert.ToString(objDatareader.GetValue(14));
                    strProjInputRevNo = Convert.ToString(objDatareader.GetValue(15));
                    strMRRelDate = Convert.ToString(objDatareader.GetValue(16));
                    strMRAllocDate = Convert.ToString(objDatareader.GetValue(17));
                    strProjInputProcessingFlag = Convert.ToString(objDatareader.GetValue(18));
                    strEPSProcessingFlag = Convert.ToString(objDatareader.GetValue(27));
                    strProjCoordinatorEmpNo = Convert.ToString(objDatareader.GetValue(19));
                    strEnggApprover = Convert.ToString(objDatareader.GetValue(20));
                    strEnggPerformer = Convert.ToString(objDatareader.GetValue(21));
                    strBiddingType = Convert.ToString(objDatareader.GetValue(22));
                    strCPSupervisor = Convert.ToString(objDatareader.GetValue(23));
                    strCPPerformer = Convert.ToString(objDatareader.GetValue(24));
                    strSubmissionFormat = Convert.ToString(objDatareader.GetValue(25));
                    strReverseAuction = Convert.ToString(objDatareader.GetValue(26));
                    strPkgFlag = Convert.ToString(objDatareader.GetValue(28));
                    strItemType = Convert.ToString(objDatareader.GetValue(29));
                }
            }

            strMRNo = strJobCode + "-" + strUnitNo + "-" + strCC + "-MR-" + strMRSlNo;

            if (strProjInputProcessingFlag == "N")
            {
                strProjInputProcessingFlagDesc = "Normal";
            }
            else if (strProjInputProcessingFlag == "G")
            {
                strProjInputProcessingFlagDesc = "Group";
            }
            else if (strProjInputProcessingFlag == "B")
            {
                strProjInputProcessingFlagDesc = "Bottom Line";
            }

            if (strEPSProcessingFlag == "N")
            {
                strEPSProcessingFlagDesc = "Normal";
            }
            else if (strEPSProcessingFlag == "G")
            {
                strEPSProcessingFlagDesc = "Group";
            }
            else if (strEPSProcessingFlag == "B")
            {
                strEPSProcessingFlagDesc = "Bottom Line";
            }

            if (strEnquiryModeCode == "LIM")
            {
                strEnquiryModeCodeDesc = "Limited";
            }
            else if (strEnquiryModeCode == "NIT")
            {
                strEnquiryModeCodeDesc = "NIT";
            }

            objDatareader.Close();
            objDatareader.Dispose();
            objConnection.Close();
            objConnection.Dispose();
        }
        public bool isEmployeeMRSupervisor(string jobCode, int rfqNo, string empNo)
        {
            MRInfo mrInfo = new MRInfo();
            mrInfo.GetMRInfoFromMRHeaderByRFQNo(jobCode, rfqNo);
            return mrInfo.strCPSupervisor == empNo;
        }
    }


}
