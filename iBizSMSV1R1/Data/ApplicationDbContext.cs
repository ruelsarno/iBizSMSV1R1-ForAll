using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using iBizSMSV1R1.Models;
using iBizSMSV1R1.ModelsAccounting;
using iBizSMSV1R1.ModelsClassroom;
using iBizSMSV1R1.ModelsAdmission;

namespace iBizSMSV1R1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<iBizSMSV1R1.Models.Reservation> Reservation { get; set; }

        public DbSet<iBizSMSV1R1.Models.StudentInfo> StudentInfo { get; set; }

        public DbSet<iBizSMSV1R1.Models.Enrolment> Enrolment { get; set; }

        public DbSet<iBizSMSV1R1.Models.Nationality> Nationality { get; set; }

        public DbSet<iBizSMSV1R1.Models.SchoolYear> SchoolYear { get; set; }

        public DbSet<iBizSMSV1R1.Models.StudentType> StudentType { get; set; }

        public DbSet<iBizSMSV1R1.Models.Studentlevel> Studentlevel { get; set; }

        public DbSet<iBizSMSV1R1.Models.GradeYear> GradeYear { get; set; }

        public DbSet<iBizSMSV1R1.Models.Section> Section { get; set; }

        public DbSet<iBizSMSV1R1.Models.TrackCode> TrackCode { get; set; }

        public DbSet<iBizSMSV1R1.Models.Discount> Discount { get; set; }

        public DbSet<iBizSMSV1R1.Models.ModeOfPayment> ModeOfPayment { get; set; }

        public DbSet<iBizSMSV1R1.Models.CivilStatus> CivilStatus { get; set; }

        public DbSet<iBizSMSV1R1.Models.Gender> Gender { get; set; }

        public DbSet<iBizSMSV1R1.Models.SchoolType> SchoolType { get; set; }

        public DbSet<iBizSMSV1R1.Models.StatusOfPayment> StatusOfPayment { get; set; }

        public DbSet<iBizSMSV1R1.Models.BillName> BillName { get; set; }

        public DbSet<iBizSMSV1R1.Models.PaymentType> PaymentType { get; set; }

        public DbSet<iBizSMSV1R1.Models.PaymentOffice> PaymentOffice { get; set; }

        public DbSet<iBizSMSV1R1.Models.Payment> Payment { get; set; }

        public DbSet<iBizSMSV1R1.Models.ToPay> ToPay { get; set; }

        public DbSet<iBizSMSV1R1.Models.BillToPay> BillToPay { get; set; }

        public DbSet<iBizSMSV1R1.Models.ClassSchedule> ClassSchedule { get; set; }

        public DbSet<iBizSMSV1R1.Models.SubjectCategory> SubjectCategory { get; set; }

        public DbSet<iBizSMSV1R1.Models.Subject> Subject { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPages> WebPages { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageSubs> WebPageSubs { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageTitles> WebPageTitles { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageContact> WebPageContact { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageSuccessStory> WebPageSuccessStory { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageEvent> WebPageEvent { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageCourse> WebPageCourse { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageBlog> WebPageBlog { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageTeacher> WebPageTeacher { get; set; }

        public DbSet<iBizSMSV1R1.Models.ProcessIntro> ProcessIntro { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAccounting.FeeCategory> FeeCategory { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAccounting.FeeDescription> FeeDescription { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAccounting.FeeTable> FeeTable { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAccounting.AccountDetail> AccountDetail { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAccounting.BankAccount> BankAccount { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageTeacherDetail> WebPageTeacherDetail { get; set; }

        public DbSet<iBizSMSV1R1.Models.ClassVideo> ClassVideo { get; set; }

        public DbSet<iBizSMSV1R1.ModelsClassroom.ClassAnnouncement> ClassAnnouncement { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageEventDetail> WebPageEventDetail { get; set; }

        public DbSet<iBizSMSV1R1.Models.ClassModule> ClassModule { get; set; }

        public DbSet<iBizSMSV1R1.Models.ClassLiveStream> ClassLiveStream { get; set; }

        public DbSet<iBizSMSV1R1.Models.StudentImage> StudentImage { get; set; }

        public DbSet<iBizSMSV1R1.Models.StudentDocument> StudentDocument { get; set; }

        public DbSet<iBizSMSV1R1.Models.StudentRelative> StudentRelative { get; set; }

        public DbSet<iBizSMSV1R1.Models.StudentContact> StudentContact { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageAbout> WebPageAbout { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageCourseDetail> WebPageCourseDetail { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAdmission.EnrollmentProdecure> EnrollmentProdecure { get; set; }

        public DbSet<iBizSMSV1R1.ModelsAdmission.AdmissionRequirement> AdmissionRequirement { get; set; }

        public DbSet<iBizSMSV1R1.Models.WebPageAchievement> WebPageAchievement { get; set; }

        
    }
}
