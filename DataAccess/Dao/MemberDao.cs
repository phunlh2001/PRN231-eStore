using BusinessObjects;
using BusinessObjects.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao
{
    public class MemberDao
    {
        #region Singleton Intance
        private static MemberDao instance;
        private static readonly object instanceLock = new object();
        public static MemberDao GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDao();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public IEnumerable<Member> GetAll()
        {
            List<Member> mems;
            try
            {
                using var context = new AppDbContext();
                mems = context.Members
                    .Include(m => m.Orders)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return mems;
        }

        public Member GetOne(int id)
        {
            Member member;
            try
            {
                using var context = new AppDbContext();
                member = context.Members
                    .Include(m => m.Orders)
                    .FirstOrDefault(m => m.MemberId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return member;
        }

        public void Insert(Member member)
        {
            try
            {
                Member _mem = GetOne(member.MemberId);
                if (_mem == null)
                {
                    using var context = new AppDbContext();
                    context.Members.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Member member)
        {
            try
            {
                Member _mem = GetOne(member.MemberId);
                if (_mem != null)
                {
                    using var context = new AppDbContext();
                    context.Members.Update(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Member _mem = GetOne(id);
                if (_mem != null)
                {
                    using var context = new AppDbContext();
                    context.Members.Remove(_mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Login(string email, string pwd)
        {
            Member member;
            try
            {
                using var context = new AppDbContext();
                member = context.Members.FirstOrDefault(m => email.Equals(m.Email));
                return !(member == null || member.Password != pwd);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool GetEmail(string email)
        {
            Member member;
            using var context = new AppDbContext();
            member = context.Members.FirstOrDefault(m => email.Equals(m.Email));
            return member != null;
        }

        public Member GetMemberByEmail(string email)
        {
            Member member;
            using var context = new AppDbContext();
            member = context.Members.FirstOrDefault(m => email.Equals(m.Email));
            return member;
        }
    }
}
