using BusinessObjects.Objects;
using System.Collections.Generic;

namespace DataAccess.Repository.Interface
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetList();
        Member GetMemberById(int id);
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int id);
        bool Login(string email, string pwd);
        bool CheckEmail(string email);
        Member GetMemberByEmail(string email);
    }
}
