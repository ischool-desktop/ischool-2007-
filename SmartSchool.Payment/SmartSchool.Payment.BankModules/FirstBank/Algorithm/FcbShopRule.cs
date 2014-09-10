using System;
using System.Collections.Generic;
using System.Text;

namespace FirstBankPayment.FirstBank.Algorithm
{
    /// <summary>
    /// �����O�t�d���ͲĤ@�Ȧ�b�W�ӳq�����T�յ����b���C�]�]�t�������ҽX���W�h�C
    /// </summary>
    public class FcbShopRule
    {
        private string _bankCode = "";  //�Ĥ@�ȶW�Ӫ��N�X�A�̪��B���O�]�w
        //private string _expiredDateStringMMDD = "";   //�I���������A�O���H�ݪ��A�X�{�A�ĤT�ӱ��X�C
        private DateTime _expiredDate;   //�I�������~���C
        private int _final_bill;
        private string _chainChargeOnus;

        /// <summary>
        /// �إ߶W�ӱ��X���ͪ���C
        /// </summary>
        /// <param name="virtualAccount">�����b���C</param>
        /// <param name="expiredDate">ú�O�L����C</param>
        /// <param name="amount">��ú���B</param>
        /// <param name="chainChargeOnus">����O�t��</param>
        public FcbShopRule(VirtualAccount virtualAccount, DateTime expiredDate, int amount, string chainChargeOnus)
        {
            //�����b����T�C
            _va = virtualAccount;

            //��ú���B
            _final_bill = amount;

            //�W�Ӫ��Ȧ�N�X�C
            _bankCode = GetBankCode(_final_bill);

            //ú�O�L����C
            _expiredDate = expiredDate;

            _chainChargeOnus = chainChargeOnus;
        }

        private VirtualAccount _va;
        public VirtualAccount VirtualAccount
        {
            get { return _va; }
        }

        public string GetCode1()
        {
            //�I���(6)+�W�Ӫ��Ȧ�N�X(3)
            return GetExpiredDateString() + this._bankCode;
            //return "9912316Y1";
        }

        public string GetCode2()
        {
            //ATM���״ڤ������b��(16)
            return VirtualAccount.ToString();
        }

        public string GetCode3()
        {
            //�I����(4) + �ˮֽX(2) +  ���B(9)
            // return _expiredDateStringMMDD + GetValidationCode() + GetBillString();
            return GetExpiredDateMMDD() + GetValidationCode() + GetBillString();
        }

        private string GetBankCode(int bill)
        {
            string result;

            if (_chainChargeOnus == "Payee")  //�Ǯխt��
            {
                if (bill >= 1 && bill <= 20000)
                    result = "6K1";
                else if (bill >= 20001 && bill <= 40000)
                    result = "6L1";
                else if (bill >= 40001 && bill <= 60000)
                    result = "6J4";
                else throw new ArgumentException("���B���b���w���d�򤺡C");
            }
            else
            {
                if (bill >= 1 && bill <= 20000)
                    result = "6X1";
                else if (bill >= 20001 && bill <= 40000)
                    result = "6Y1";
                else if (bill >= 40001 && bill <= 60000)
                    result = "6J8";
                else throw new ArgumentException("���B���b���w���d�򤺡C");
            }
            return result;
        }

        /// <summary>
        /// �W�Ӫ����O�����r��Ayymmdd������~���C
        /// </summary>
        /// <returns></returns>
        private string GetExpiredDateString()
        {
            int year = (_expiredDate.Year > 1911) ? (_expiredDate.Year - 1911) : (_expiredDate.Year);
            year = year % 100; //�����
            return Utility.GetWellFormedString(year.ToString(), 2, "") + GetExpiredDateMMDD();
        }

        /// <summary>
        /// �I���(��B��)�C
        /// </summary>
        /// <returns></returns>
        private string GetExpiredDateMMDD()
        {
            return Utility.GetWellFormedString(_expiredDate.Month.ToString(), 2, "") +
                        Utility.GetWellFormedString(_expiredDate.Day.ToString(), 2, "");
        }

        /// <summary>
        /// ���o�W�Ӫ��ˬd�X�A�@��X�C
        /// �Ĥ@�X���W�h���G�U�qBarcode����ƭȡ����[�`�� ���H11 �ᤧ���l�ơ� (�Y�l�Ƭ�0�h��A,�Y�l�Ƭ�10�h��B)
        /// �ĤG�X�p�⤽���G�U�qBarcode�����ƭȡ����[�`�� ���H11 �ᤧ���l�ơ� (�Y�l�Ƭ�0 �h��X, �Y�l�Ƭ�10�h��Y)
        /// </summary>
        /// <returns></returns>
        public string GetValidationCode()
        {
            int oddSum = 0;
            int evenSum = 0;

            //for Shop BarCode 1
            string bar1 = this.GetCode1();
            for (int i = 0; i < bar1.Length; i++)
            {
                int val = this.GetCharValue(bar1.Substring(i, 1));
                if (i % 2 == 0)
                    oddSum += val;
                else
                    evenSum += val;
            }

            //for Shop BarCode 2
            string bar2 = this.GetCode2();
            for (int i = 0; i < bar2.Length; i++)
            {
                int val = this.GetCharValue(bar2.Substring(i, 1));
                if (i % 2 == 0)
                    oddSum += val;
                else
                    evenSum += val;
            }

            //for Shop BarCode 3
            //string bar3 = this._expiredDateStringMMDD + "00" + GetBillString();    //�����["00"�O�]��0���v�T�[�`���G
            string bar3 = this.GetExpiredDateMMDD() + "00" + GetBillString();    //�����["00"�O�]��0���v�T�[�`���G
            for (int i = 0; i < bar3.Length; i++)
            {
                int val = this.GetCharValue(bar3.Substring(i, 1));
                if (i % 2 == 0)
                    oddSum += val;
                else
                    evenSum += val;
            }

            //�p��Ĥ@�X�A�_�ƩM�Q11�����l�ơA0�h��A, 10�h��B�A��l���Ʀr�C            
            int valCode1 = oddSum % 11;
            string validatationCode1 = (valCode1 == 0) ? "A" : ((valCode1 == 10) ? "B" : valCode1.ToString());

            //�p��ĤG�X�A���ƩM�Q11�����l�ơA0�h��X, 10�h��Y�A��l����Ʀr�C     
            int valCode2 = evenSum % 11;
            string validatationCode2 = (valCode2 == 0) ? "X" : ((valCode2 == 10) ? "Y" : valCode2.ToString());

            return validatationCode1 + validatationCode2;
        }

        /// <summary>
        /// �W�Ӫ����B���E��ơA�n�a�k���ɹs�C
        /// </summary>
        /// <returns></returns>
        private string GetBillString()
        {
            return Utility.GetWellFormedString(_final_bill.ToString(), 9, "�W�Ӫ��B����W�L9��ơI");
            //return "000007890";
        }

        /// <summary>
        /// �W�ӱ��X���i�঳�^��r���A�b�p���ˮֽX�ɶ��N���ର�Ʀr�p��C
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private int GetCharValue(string chr)
        {
            /*
              ��A��:1, ��B��:2, ��C��:3, ��D��:4, ��E��:5, ��F��:6, ��G��:7, ��H��:8, ��I��:9
              ��J��:1, ��K��:2, ��L��:3, ��M��:4, ��N��:5, ��O��:6, ��P��:7, ��Q��:8, ��R��:9, 
              ��S��:2 , ��T��:3, ��U��:4, ��V��:5, ��W��;6, ��X��:7, ��Y��:8, ��Z��:9
             * */
            int result = -1;
            string flag = chr.ToUpper();

            if ((flag == "A") || (flag == "J") || (flag == "1"))
                result = 1;
            else if ((flag == "B") || (flag == "K") || (flag == "S") || (flag == "2"))
                result = 2;
            else if ((flag == "C") || (flag == "L") || (flag == "T") || (flag == "3"))
                result = 3;
            else if ((flag == "D") || (flag == "M") || (flag == "U") || (flag == "4"))
                result = 4;
            else if ((flag == "E") || (flag == "N") || (flag == "V") || (flag == "5"))
                result = 5;
            else if ((flag == "F") || (flag == "O") || (flag == "W") || (flag == "6"))
                result = 6;
            else if ((flag == "G") || (flag == "P") || (flag == "X") || (flag == "7"))
                result = 7;
            else if ((flag == "H") || (flag == "Q") || (flag == "Y") || (flag == "8"))
                result = 8;
            else if ((flag == "I") || (flag == "R") || (flag == "Z") || (flag == "9"))
                result = 9;
            else if (flag == "0")
                result = 0;

            return result;
        }
    }

}
