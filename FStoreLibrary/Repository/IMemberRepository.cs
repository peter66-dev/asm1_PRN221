using FStoreLibrary.DataAccess;
using System.Collections.Generic;

namespace FStoreLibrary.Repository
{
    public interface IMemberRepository
    {
        public List<Member> GetAllMembers();
        public Member GetMemberByID(int id);
        public List<Member> GetMembersByEmail(string email);
        public bool UpdateMember(Member member);
        public bool DeleteMember(int id);
        public bool InsertMember(Member mem);
        public Member GetMemberByEmail(string email);
    }
}
