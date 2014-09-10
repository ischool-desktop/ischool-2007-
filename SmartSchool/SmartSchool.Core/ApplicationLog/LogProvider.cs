using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SmartSchool.ApplicationLog
{
    public class LogProvider
    {
        private LogStorageQueue _storage;
        private string _user_name;

        internal LogProvider(LogStorageQueue storage, string userName)
        {
            _storage = storage;
            _user_name = userName;
        }

        public void Write(ILogInformation log)
        {
            log.Content.UserName = _user_name;
            log.Content.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo); //2006-04-11 00:00:00
            log.Flush();

            lock (_storage.SyncRoot)
            {
                _storage.Enqueue(log);
            }
        }

        /// <summary>
        /// 寫入與 Entity 相關的應用程式記錄。
        /// </summary>
        /// <param name="entity">相關 Entity。</param>
        /// <param name="action">動作。</param>
        /// <param name="identity">Entity 的唯一識別碼(PrimaryKey)。</param>
        /// <param name="source">動作來源。</param>
        /// <param name="desc">詳細說明。</param>
        /// <param name="diagnostics">除錯資訊。</param>
        public void Write(EntityType entity, EntityAction action, string identity, string desc, string source, string diagnostics)
        {
            GeneralEntityLog entitylog = new GeneralEntityLog();

            if (entity == EntityType.Undefine)
                throw new ArgumentException("必須指定 EntityType(不可以使用 Undefine)。", "entity");

            if (action == EntityAction.Undefine)
                throw new ArgumentException("必須指定 EntityAction(不可以使用 Undefine)。", "action");

            InternalWrite(entity, EntityActionName.Get(action), identity, desc, source, diagnostics, entitylog);
        }

        /// <summary>
        /// 寫入與 Entity 相關的應用程式記錄。
        /// </summary>
        /// <param name="entity">相關 Entity。</param>
        /// <param name="action">動作。</param>
        /// <param name="identity">Entity 的唯一識別碼(PrimaryKey)。</param>
        /// <param name="source">動作來源。</param>
        /// <param name="desc">詳細說明。</param>
        /// <param name="diagnostics">除錯資訊。</param>
        public void Write(EntityType entity, string action, string identity, string desc, string source, string diagnostics)
        {
            GeneralEntityLog entitylog = new GeneralEntityLog();

            if (entity == EntityType.Undefine)
                throw new ArgumentException("必須指定 EntityType(不可以使用 Undefine)。", "entity");

            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("必須指定 EntityAction。", "action");

            InternalWrite(entity, action, identity, desc, source, diagnostics, entitylog);
        }

        private void InternalWrite(EntityType entity, string action, string identity, string desc, string source, string diagnostics, GeneralEntityLog entitylog)
        {
            entitylog.EntityName = EntityTypeName.Get(entity);
            entitylog.ActionName = action;
            entitylog.EntityID = identity;
            entitylog.Description = desc;
            entitylog.Source = source;
            entitylog.Diagnostics = diagnostics;

            ILogInformation log = entitylog as ILogInformation;
            log.Content.UserName = _user_name;
            log.Content.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo); //2006-04-11 00:00:00
            log.Flush();

            lock (_storage.SyncRoot)
            {
                _storage.Enqueue(entitylog);
            }
        }

        /// <summary>
        /// 寫入一般的應用程式記錄。
        /// </summary>
        /// <param name="action">動作名稱。</param>
        /// <param name="source">動作來源。</param>
        /// <param name="desc">詳細說明。</param>
        /// <param name="diagnostics">除錯資訊。</param>
        public void Write(string action, string desc, string source, string diagnostics)
        {
            GeneralActionLog actionlog = new GeneralActionLog();
            actionlog.ActionName = action;
            actionlog.Description = desc;
            actionlog.Source = source;
            actionlog.Diagnostics = diagnostics;

            ILogInformation log = actionlog as ILogInformation;
            log.Content.UserName = _user_name;
            log.Content.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo); //2006-04-11 00:00:00
            log.Flush();

            lock (_storage.SyncRoot)
            {
                _storage.Enqueue(actionlog);
            }
        }
    }
}