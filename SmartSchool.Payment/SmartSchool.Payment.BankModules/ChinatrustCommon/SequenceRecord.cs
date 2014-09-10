using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Private.UDT;

namespace AccountsReceivalbe.BuildinBank.ChinatrustCommon
{
    /// <summary>
    /// 用於儲存產生條碼時的流水編號。
    /// </summary>
    [TableName("SequenceRecord.Chinatrust")]
    internal class SequenceRecord : ActiveRecord
    {
        [Field()]
        public int Sequence { get; set; }

        /// <summary>
        /// SequenceRecord 在資料庫中只允許一筆資料，如果有一筆以上的資料，那就爆給他看。
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static int GetNextSequence()
        {
            AccessHelper udt = new AccessHelper();
            List<SequenceRecord> records = udt.Select<SequenceRecord>();

            if (records.Count <= 1)
            {
                if (records.Count == 0)
                {
                    SequenceRecord record = new SequenceRecord();
                    record.Sequence = 0;

                    udt.InsertValues(new SequenceRecord[] { record });

                    return record.Sequence;
                }
                else if (records.Count == 1)
                {
                    return records[0].Sequence;
                }
            }

            throw new InvalidOperationException("資料庫中的流水號狀態不正確，請連絡系統管理人員(GetNextSequence)。");
        }

        /// <summary>
        /// SequenceRecord 在資料庫中只允許一筆資料，如果有一筆以上的資料，那就爆給他看。
        /// </summary>
        /// <param name="originSequence">產生條碼前的流水編號，用於判斷是否有其他使用者修改過流水編號。</param>
        /// <param name="sequence">要儲存的流水編號。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static void SaveSequence(int originSequence, int sequence)
        {
            if (GetNextSequence() == originSequence)
            {
                SaveSequence(sequence);
            }
            else
                throw new InvalidOperationException("銀行模組設定已被其他使用者變更。");
        }

        /// <summary>
        /// SequenceRecord 在資料庫中只允許一筆資料，如果有一筆以上的資料，那就爆給他看。
        /// </summary>
        /// <param name="sequence">要儲存的流水編號。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static void SaveSequence(int sequence)
        {
            AccessHelper udt = new AccessHelper();
            List<SequenceRecord> records = udt.Select<SequenceRecord>();

            if (records.Count == 1)
            {
                SequenceRecord record = records[0];
                record.Sequence = sequence;

                udt.UpdateValues(new SequenceRecord[] { record });
            }
            else
                throw new InvalidOperationException("資料庫中的流水號狀態不正確，請連絡系統管理人員(SaveSequence)。");
        }
    }
}
