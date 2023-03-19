using Intentio.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Data.SqlClient;

namespace Intentio
{
    public class Database : IDisposable
    {
        private List<IUser> _users = new List<IUser>();
        private static object sync = new object();
        private static string Path = "users.json";

        public Database()
        {
            lock (sync)
            {
                if (!File.Exists(Path))
                {
                    using var fs = File.Create(Path);
                    fs.Write(Encoding.UTF8.GetBytes("[]"));
                }

                var bytes = File.ReadAllBytes(Path);
                var dbstr = Encoding.UTF8.GetString(bytes);
                _users = JsonSerializer.Deserialize<List<IUser>>(dbstr);
            }
        }

#nullable enable
        public IUser? GetByDevice(Device device)
        {
            //throw new NotImplementedException();
            lock (sync)
            {
                foreach (var user in _users)
                {
                    if (user.Identifier.Address == device.Address) return user;
                }

                return null;
            }
        }
#nullable disable
        public IUser AddOrUpdate(IUser user)
        {
            lock (sync)
            {

                for (int i = 0; i < _users.Count; i++)
                {
                    IUser existingUser = _users[i];
                    if (user.Identifier.Address == existingUser.Identifier.Address)
                    {
                        _users[i] = user;
                        return user;
                    }
                }

                _users.Add(user);
                return user;
            }
        }


        public void Dispose()
        {
            lock (sync)
            {
                var dbstr = JsonSerializer.Serialize(_users);
                var dbbytes = Encoding.UTF8.GetBytes(dbstr);
                File.WriteAllBytes(Path, dbbytes);
                GC.SuppressFinalize(this);
            }
        }
    }
}
