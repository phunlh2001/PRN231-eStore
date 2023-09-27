using BusinessObjects.Objects;
using DataAccess.Dao;
using DataAccess.Repository.Interface;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public bool CheckEmail(string email) => MemberDao.GetInstance.GetEmail(email);

        public void DeleteMember(int id) => MemberDao.GetInstance.Delete(id);

        public IEnumerable<Member> GetList() => MemberDao.GetInstance.GetAll();

        public Member GetMemberByEmail(string email) => MemberDao.GetInstance.GetMemberByEmail(email);

        public Member GetMemberById(int id) => MemberDao.GetInstance.GetOne(id);

        public void InsertMember(Member member) => MemberDao.GetInstance.Insert(member);

        public bool Login(string email, string pwd) => MemberDao.GetInstance.Login(email, pwd);

        public void UpdateMember(Member member) => MemberDao.GetInstance.Update(member);
    }
}
