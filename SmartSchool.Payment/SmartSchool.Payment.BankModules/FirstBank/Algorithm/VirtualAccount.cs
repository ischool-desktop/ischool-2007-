using System;
using System.Collections.Generic;
using System.Text;

namespace FirstBankPayment.FirstBank.Algorithm
{
    /// <summary>
    /// �����O�t�d���� �@�� �����b���C�]�]�t�������ҽX���W�h�C
    /// </summary>
    public class VirtualAccount
    {
        private string _origin_unique_id;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cropCode">���~�N�X</param>
        /// <param name="bill">ú�ڪ��B</param>
        /// <param name="expriedDate">�I����</param>
        /// <param name="uniqueId">�ߤ@�ѧO�X�C�ثe�ĥ� �p�� 99999���y�����C</param>
        public VirtualAccount(string cropCode, int bill, DateTime expriedDate, string uniqueId)
        {
            _corpCode = cropCode;
            _bill = bill;
            _expiredDate = expriedDate;
            _origin_unique_id = uniqueId;
            _uniqueId = Utility.GetWellFormedString(_origin_unique_id, 6, "");
        }

        /// <summary>
        /// �p�G���ǮեN�X�ɡA���򲣥�ATM�����b������10�X���ǮեN�X�C
        /// </summary>
        /// <param name="schoolCode"></param>
        public void SetSchoolCode(string schoolCode)
        {
            _school_code = schoolCode;
            _uniqueId = SchoolCode + Utility.GetWellFormedString(_origin_unique_id, 5, "");
        }


        private string _school_code;
        /// <summary>
        /// �u����@�ӾǮժ���]�������z�L�ۦP�����~�N�X���O�ɤ~�|�Ψ�SchoolCode�C�Y�Ǯեu���鶡���ɡA�N���ݭn�]�wSchoolCode�CSchoolCode�ѨϥΪ̦b�e���W�]�w�A���׬��@�Ӧr���C
        /// </summary>
        public string SchoolCode
        {
            get { return _school_code; }
        }

        private string _corpCode;
        /// <summary>
        /// ���~�N�X�C
        /// </summary>
        public string CorporationCode
        {
            get { return _corpCode; }
        }

        private int _bill;
        /// <summary>
        /// ú�O���B
        /// </summary>
        private int Bill
        {
            get { return _bill; }
        }

        private DateTime _expiredDate;
        /// <summary>
        /// ú�O�I���
        /// </summary>
        private DateTime ExpiredDate
        {
            get { return _expiredDate; }
        }

        private string _uniqueId = "";
        public string UniqueID
        {
            get { return _uniqueId; }
        }

        /// <summary>
        /// ���ͲĤ@�Ȧ� �����N�X�A�@16�X�C
        /// �����N�X(5) + �����~(1) + ������(3) + �s��(6) + �ˮֽX(1) = 16�X
        /// </summary>
        /// <returns></returns>
        public string GetVirtualAccount()
        {
            //�����b���W�h���G
            //�����N�X(5) + �����~(1) + ������(3) + �s��(6) + �ˮֽX(1) = 16�X
            return CorporationCode + GetExprYearString() + GetExprDayString() + UniqueID + GetValidationCode();
        }

        /// <summary>
        /// ���o�Ĥ@�Ȧ� �����N�X�A�@16�X�C
        /// �����N�X(5) + �����~(1) + ������(3) + �s��(6) + �ˮֽX(1) = 16�X
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetVirtualAccount();
        }

        /// <summary>
        /// ���o�����~
        /// </summary>
        /// <returns></returns>
        private string GetExprYearString()
        {
            return (ExpiredDate.Year % 10).ToString();
        }

        /// <summary>
        /// ���o������
        /// </summary>
        /// <returns></returns>
        private string GetExprDayString()
        {
            return Utility.GetWellFormedString(ExpiredDate.DayOfYear.ToString(), 3, "");
        }

        /// <summary>
        /// �p�� ATM�����b�����ˮֽX
        /// </summary>
        /// <param name="corpCode"></param>
        /// <param name="id"></param>
        /// <param name="bill"></param>
        /// <returns></returns>
        public string GetValidationCode()
        {
            //�Ĥ@�����ҡA�N�U�C�U��ƨ̧ǭ��H ( 3,7,1,3,7,1,3,7,1, ...... )
            string tempCode = CorporationCode + GetExprYearString() + GetExprDayString() + UniqueID;
            int[] ary1 = { 3, 7, 1 };
            int x1 = 0;
            for (int i = 0; i < tempCode.Length; i++)
            {
                x1 += ary1[i % 3] * int.Parse(tempCode.Substring(i, 1));
            }
            x1 = x1 % 10;

            //�ĤG�����ҡA���o���B�A�U�Ӧ�ƨ̧ǭ��H8�������Ʀr(8,7,6,5,4,3,2,1)
            string billString = Utility.GetWellFormedString(Bill.ToString(), 8, "���B����W�L8���");
            int[] ary2 = { 8, 7, 6, 5, 4, 3, 2, 1 };
            int x2 = 0;
            for (int i = 0; i < billString.Length; i++)
            {
                x2 += ary2[i] * int.Parse(billString.Substring(i, 1));
            }

            int result = 10 - ((x1 + x2) % 10);

            result = (result == 10) ? 0 : result;

            return result.ToString();
        }

        //ATM��b�ɪ��Ȧ�N�X�A�Ĥ@�Ȧ欰 007
        private static string BankCode
        {
            get { return "007"; }
        }

    }
}
