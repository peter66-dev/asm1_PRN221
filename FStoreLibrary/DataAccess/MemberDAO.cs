using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FStoreLibrary.DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Member> GetAllMembers()
        {
            List<Member> members = null;
            try
            {
                var context = new FStoreDBContext();
                members = context.Members.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Bug in GetAllMembers function!");
            }
            return members;
        }

        public Member GetMemberByID(int id)
        {
            Member mem = null;
            try
            {
                var context = new FStoreDBContext();
                mem = context.Members.SingleOrDefault(m => m.MemberId == id);
            }
            catch (Exception)
            {
                throw new Exception("Bug in GetMemberByID function!");
            }
            return mem;
        }

        public List<Member> GetMembersByEmail(string email)
        {
            List<Member> list = null;
            try
            {
                var context = new FStoreDBContext();
                list = context.Members.Where(m => m.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in GetMembersByEmail function!");
            }
            return list;
        }

        public bool UpdateMember(Member mem)
        {
            bool isUpdated = false;
            try
            {
                Member tmp = GetMemberByID(mem.MemberId);
                if (tmp != null)
                {
                    var context = new FStoreDBContext();
                    context.Entry<Member>(mem).State = EntityState.Modified;
                    if (context.SaveChanges() != 0)
                    {
                        isUpdated = !isUpdated;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in UpdateMember function!");
            }
            return isUpdated;
        }

        public bool DeleteMember(int id)
        {
            bool isDeleted = false;
            try
            {
                Member mem = GetMemberByID(id);
                if (mem != null)
                {
                    var context = new FStoreDBContext();
                    context.Members.Remove(mem);
                    if (context.SaveChanges() != 0)
                    {
                        isDeleted = !isDeleted;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bug in DeleteMember function!");
            }
            return isDeleted;
        }
        public bool InsertMember(Member mem)
        {
            bool isInserted = false;
            try
            {
                Member tmp = GetMemberByID(mem.MemberId);
                if(tmp == null)
                {
                    var context = new FStoreDBContext();
                    context.Members.Add(mem);
                    if(context.SaveChanges() != 0)
                    {
                        isInserted = !isInserted;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("MemberID has existed in system!");
            }
            return isInserted;
        }

    }
}