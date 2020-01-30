using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Switcher.utils
{
    class CertManager
    {



        
        /*
         * 
         * Получение статусов
         * Таск/Напрямую
         */
        public bool GetStatus()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var c = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
            bool res = c.Count >= 1;
            store.Close();
            return res;
        }

        public Task<bool> StatusAsync()
        {
            return Task.Run<bool>(() => GetStatus());
        }

        /*
         * 
         * Установка/Удаление сертификата
         * 
         */

        public void install()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite);

            var cert = new X509Certificate2(Switcher.Properties.Resources.minase);
            store.Add(cert);

            store.Close();
        }

        public void uninstall()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);

            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);

            foreach (var cert in certs)
            {
                try
                {
                    store.Remove(cert);
                }catch (Exception ex)
                {
                    throw ex;
                }
            }

            if(store != null)
            {
                store.Close();
            }
        }
    }
}
