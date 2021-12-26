using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using iBizSMSV1R1.Functions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace iBizSMSV1R1.Interfaces
{    
    public interface IStudent
    {
        IEnumerable<StudentInfo> GetAllStudentInfo();
        StudentInfo GetStudentInfoByID(string id);
        StudentInfo GetStudentInfoBySchoolYear(string schoolyear);
        StudentInfo Add(StudentInfo studentinfo);
        StudentInfo Update(StudentInfo studentinfochanges);
        StudentInfo Delete(string id);
    }
}
