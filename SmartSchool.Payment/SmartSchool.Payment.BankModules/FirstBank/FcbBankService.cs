using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Payment.Interfaces;

namespace FirstBankPayment.FirstBank
{
    /// <summary>
    /// �����O��@IBankService�A���Ѭ��������O�θ�T���D�{���C
    /// </summary>
    /// <remarks>
    /// �����O�OAbstract Factory Pattern ���� Concrete Factory�A�t�d���ͬ�������A�ô��ѬY�@�Ȧ檺�S�w��T�C
    /// </remarks>
    public class FcbBankService : IBankService
    {
        /// <summary>
        /// ���ݩʧ@���x�s���n�]�w�ɪ� key �C���P�Ȧ��������P�� Key �ȡC
        /// </summary>
        public const string PreferenceIdentity = "SmartSchool.Payment.BankModules.FirstBank";

        /// <summary>
        /// �y�������̤j�ȡA�Фŭק�C
        /// </summary>
        public const int MaxSequence = 99999;

        #region IBankService ����

        /// <summary>
        /// ���o�ҲզW�١A��ĳ�H�Ȧ檺����W�١C
        /// </summary>
        public string Name
        {
            get { return "�Ĥ@�Ȧ�(���}��)"; }
        }

        /// <summary>
        /// ���o���Ҳժ��N�X�C���P�Ȧ����w�q���P���N�X�A��ĳ�H�Ȧ檺�^��W�١C
        /// </summary>
        public string ModuleCode
        {
            get { return "FirstBank"; }
        }

        /// <summary>
        /// �إߦ��~�� BankConfigPane ���O�A�B������H�U�M���޿誺����C
        /// ������O��User Control�A�ΨӺ޲z�Y�@�Ȧ�b�Y���B�ŶZ����������O�Υ��~�N�X�C
        /// </summary>
        /// <param name="preConf">BankConfig����A�N��Y�@�Ȧ�b�Y���B�ŶZ����������O�Υ��~�N�X���պA�ȡC(�պA�Ȯ榡�i�Ѧ� Config_Sample.xml ) </param>
        /// <returns></returns>
        public BankConfigPane CreateBankConfigPane(BankConfig preConf)
        {
            BankConfigPane pane = new FcbBankConfigPane();
            pane.SetConfig(preConf);
            return pane;
        }

        /// <summary>
        /// �إߦ���@ IBillCodeGenerator ����������C
        /// </summary>
        /// <param name="bankConf">�Y�Ȧ檺�պA�ȡC(�պA�Ȯ榡�i�Ѧ� Config_Sample.xml ) </param>
        /// <param name="expireation">ú�O�I���</param>
        /// <returns></returns>
        public IBillCodeGenerator CreateGenerator(BankConfig bankConf, DateTime expireation)
        {
            return new FcbGenerator(bankConf, expireation);
        }

        /// <summary>
        /// ���o�պA�Ȥ������B�W���C
        /// </summary>
        /// <param name="bankConf">�Y�Ȧ檺�պA�ȡC(�պA�Ȯ榡�i�Ѧ� Config_Sample.xml )</param>
        /// <returns></returns>
        public int GetAmountLimit(BankConfig bankConf)
        {
            ConfigParser cparser = new ConfigParser(bankConf);
            int maxAmount = 1;

            foreach (AmountLevel each in cparser.Levels)
                maxAmount = Math.Max(maxAmount, each.UpperLimit);

            return maxAmount;
        }

        #endregion
    }

}
