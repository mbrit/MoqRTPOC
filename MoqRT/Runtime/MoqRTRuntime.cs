using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq.Baking;
using SQLite;

namespace Moq
{
    public static class MoqRTRuntime
    {
        private static string BakingDatabasePath { get; set; }
        internal static MethodItem RunningMethod { get; private set; }

        public static void ConfigureBaking(string path)
        {
            BakingDatabasePath = path;

            // setup...
            using (var conn = GetBakingConnection())
            {
                conn.CreateTable<InstanceItem>();
                conn.CreateTable<MethodItem>();
                conn.CreateTable<BakingItem>();
            }
        }

        internal static SQLiteConnection GetBakingConnection()
        {
            return new SQLiteConnection(BakingDatabasePath);
        }

        internal static bool IsBaking
        {
            get
            {
                return !(string.IsNullOrEmpty(BakingDatabasePath));
            }
        }

        public static void SetRunningMethod(object instance, MethodInfo method)
        {
            using (var conn = GetBakingConnection())
            {
                // find the instance...
                var hash = instance.GetHashCode();
                var instanceItem = conn.Table<InstanceItem>().Where(v => v.HashCode == hash).FirstOrDefault();
                if (instanceItem == null)
                {
                    instanceItem = new InstanceItem()
                    {
                        HashCode = instance.GetHashCode(),
                        Type = instance.GetType().AssemblyQualifiedName
                    };
                    conn.Insert(instanceItem);
                }

                // create the method...
                var methodItem = new MethodItem()
                {
                    InstanceItemId = instanceItem.Id,
                    Name = method.Name
                };
                conn.Insert(methodItem);

                // set...
                RunningMethod = methodItem;
            }
        }
    }
}
