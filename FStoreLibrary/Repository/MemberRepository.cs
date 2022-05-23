using FStoreLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStoreLibrary.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public bool DeleteMember(int id) => MemberDAO.Instance.DeleteMember(id);

        public List<Member> GetAllMembers() => MemberDAO.Instance.GetAllMembers();

        public Member GetMemberByID(int id) => MemberDAO.Instance.GetMemberByID(id);

        public List<Member> GetMembersByEmail(string email) => MemberDAO.Instance.GetMembersByEmail(email);

        public bool InsertMember(Member mem) => MemberDAO.Instance.InsertMember(mem);
        
        public bool UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);
        public Member GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);
    }
}
