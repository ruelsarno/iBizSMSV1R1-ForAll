using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBizSMSV1R1.Migrations
{
    public partial class initialall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountDetail",
                columns: table => new
                {
                    accountdetailrecno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idno = table.Column<string>(maxLength: 25, nullable: false),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    discountcode = table.Column<string>(maxLength: 50, nullable: true),
                    discountname = table.Column<string>(maxLength: 150, nullable: true),
                    discount = table.Column<float>(nullable: false),
                    modeofpayment = table.Column<string>(maxLength: 25, nullable: false),
                    feecategory = table.Column<string>(maxLength: 50, nullable: false),
                    feedescription = table.Column<string>(maxLength: 50, nullable: false),
                    feeamount = table.Column<float>(nullable: false),
                    amountofdiscount = table.Column<float>(nullable: false),
                    totalpayment = table.Column<float>(nullable: false),
                    rebate = table.Column<float>(nullable: false),
                    balance = table.Column<float>(nullable: false),
                    status = table.Column<string>(maxLength: 25, nullable: true),
                    remarks = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetail", x => x.accountdetailrecno);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionRequirement",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    title = table.Column<string>(maxLength: 255, nullable: false),
                    requirement = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionRequirement", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    idno = table.Column<string>(maxLength: 50, nullable: true),
                    cardid = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bankname = table.Column<string>(maxLength: 150, nullable: false),
                    branch = table.Column<string>(maxLength: 150, nullable: false),
                    accounttype = table.Column<string>(maxLength: 150, nullable: false),
                    accountno = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "BillName",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billnames = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillName", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "BillToPay",
                columns: table => new
                {
                    recno = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    referenceno = table.Column<long>(nullable: false),
                    id = table.Column<string>(nullable: false),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    billnames = table.Column<string>(maxLength: 50, nullable: false),
                    billdate = table.Column<string>(nullable: true),
                    amount = table.Column<double>(nullable: false),
                    duedate = table.Column<string>(nullable: true),
                    payment = table.Column<bool>(nullable: false),
                    proofofpayment = table.Column<byte[]>(nullable: true),
                    confirm = table.Column<bool>(nullable: false),
                    confirmedby = table.Column<string>(maxLength: 150, nullable: true),
                    notified = table.Column<bool>(nullable: false),
                    paymentupdate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillToPay", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "CivilStatus",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    civilstatus = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivilStatus", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "ClassAnnouncement",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    subjectcode = table.Column<string>(maxLength: 50, nullable: false),
                    subjectname = table.Column<string>(maxLength: 512, nullable: false),
                    proctor = table.Column<string>(maxLength: 150, nullable: false),
                    announcementtitle = table.Column<string>(maxLength: 25, nullable: false),
                    datestart = table.Column<DateTime>(nullable: false),
                    dateend = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 100, nullable: true),
                    link = table.Column<string>(maxLength: 512, nullable: true),
                    imagedata = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAnnouncement", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "ClassLiveStream",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    subjectname = table.Column<string>(maxLength: 512, nullable: false),
                    lessonno = table.Column<int>(nullable: false),
                    lessontitle = table.Column<string>(maxLength: 150, nullable: false),
                    videolink = table.Column<string>(maxLength: 150, nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLiveStream", x => x.recordno);
                });

            migrationBuilder.CreateTable(
                name: "ClassModule",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    subjectname = table.Column<string>(maxLength: 512, nullable: false),
                    lessonno = table.Column<int>(nullable: false),
                    lessontitle = table.Column<string>(maxLength: 150, nullable: false),
                    videolink = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassModule", x => x.recordno);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedule",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    studentgradeyear = table.Column<string>(maxLength: 50, nullable: false),
                    section = table.Column<string>(maxLength: 50, nullable: false),
                    subjectcode = table.Column<string>(maxLength: 150, nullable: true),
                    subjectname = table.Column<string>(maxLength: 512, nullable: true),
                    teacher = table.Column<string>(maxLength: 150, nullable: true),
                    weekday = table.Column<string>(maxLength: 50, nullable: true),
                    roomno = table.Column<string>(maxLength: 25, nullable: true),
                    starttime = table.Column<string>(nullable: true),
                    endtime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedule", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discountcodes = table.Column<string>(maxLength: 25, nullable: true),
                    discounts = table.Column<double>(nullable: false),
                    description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentProdecure",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    title = table.Column<string>(maxLength: 255, nullable: false),
                    coursetitle = table.Column<string>(maxLength: 50, nullable: true),
                    procedure = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentProdecure", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Enrolment",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idno = table.Column<string>(maxLength: 25, nullable: true),
                    id = table.Column<string>(nullable: false),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    registrationno = table.Column<string>(maxLength: 25, nullable: true),
                    registrationdate = table.Column<string>(nullable: true),
                    studenttype = table.Column<string>(maxLength: 25, nullable: false),
                    studentlevel = table.Column<string>(maxLength: 25, nullable: false),
                    trackcode = table.Column<string>(maxLength: 25, nullable: true),
                    gradeyear = table.Column<string>(maxLength: 25, nullable: true),
                    section = table.Column<string>(maxLength: 50, nullable: true),
                    discountcode = table.Column<string>(maxLength: 25, nullable: true),
                    modeofpayment = table.Column<string>(maxLength: 25, nullable: false),
                    schoollastattended = table.Column<string>(maxLength: 150, nullable: true),
                    schooltype = table.Column<string>(maxLength: 25, nullable: true),
                    confirmed = table.Column<bool>(nullable: false),
                    confirmedby = table.Column<string>(maxLength: 150, nullable: true),
                    notified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolment", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "FeeCategory",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feecategoryname = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeCategory", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "FeeDescription",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feecategory = table.Column<string>(maxLength: 50, nullable: false),
                    feename = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeDescription", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "FeeTable",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    gradeyear = table.Column<string>(maxLength: 25, nullable: false),
                    paymentmode = table.Column<string>(maxLength: 25, nullable: false),
                    tuitionfee = table.Column<float>(nullable: false),
                    reservationfee = table.Column<float>(nullable: false),
                    uponenrollment = table.Column<float>(nullable: false),
                    installmentcount = table.Column<int>(nullable: false),
                    paymentschedule = table.Column<string>(maxLength: 255, nullable: true),
                    installmentamount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeTable", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genders = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "GradeYear",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentlevels = table.Column<string>(maxLength: 25, nullable: true),
                    gradeyears = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeYear", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "ModeOfPayment",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modeofpayments = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeOfPayment", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nationality = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    id = table.Column<string>(nullable: false),
                    referenceno = table.Column<long>(nullable: false),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    billnames = table.Column<string>(maxLength: 50, nullable: false),
                    billdate = table.Column<DateTime>(nullable: false),
                    paymentdate = table.Column<DateTime>(nullable: false),
                    paymenttypes = table.Column<string>(maxLength: 50, nullable: true),
                    paymentoffices = table.Column<string>(maxLength: 150, nullable: true),
                    amountpaid = table.Column<double>(nullable: false),
                    paymentpostdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.recordno);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOffice",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentoffices = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOffice", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymenttypes = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "ProcessIntro",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    subtitle = table.Column<string>(maxLength: 150, nullable: false),
                    description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessIntro", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    referenceno = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(nullable: false),
                    studenttype = table.Column<string>(maxLength: 25, nullable: false),
                    datereserve = table.Column<string>(nullable: true),
                    idno = table.Column<string>(maxLength: 25, nullable: true),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    studentlevel = table.Column<string>(maxLength: 25, nullable: false),
                    gradeyear = table.Column<string>(maxLength: 25, nullable: false),
                    trackcode = table.Column<string>(maxLength: 25, nullable: true),
                    schoollastattended = table.Column<string>(maxLength: 150, nullable: true),
                    schooltype = table.Column<string>(maxLength: 25, nullable: true),
                    paymentstatus = table.Column<string>(maxLength: 25, nullable: true),
                    confirm = table.Column<bool>(nullable: false),
                    confirmedby = table.Column<string>(maxLength: 150, nullable: true),
                    notified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.referenceno);
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schooltypes = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYear",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schoolyears = table.Column<string>(maxLength: 25, nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYear", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gradeyears = table.Column<string>(maxLength: 25, nullable: true),
                    sections = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "StatusOfPayment",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentstatus = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOfPayment", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "StudentContact",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(nullable: false),
                    telephoneno = table.Column<string>(maxLength: 150, nullable: false),
                    cellphoneno = table.Column<string>(maxLength: 150, nullable: false),
                    emailadd = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentContact", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "StudentDocument",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(nullable: false),
                    documentname = table.Column<string>(maxLength: 150, nullable: false),
                    imagedata = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDocument", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "StudentInfo",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    idno = table.Column<string>(maxLength: 25, nullable: true),
                    lrn = table.Column<string>(maxLength: 25, nullable: true),
                    surname = table.Column<string>(maxLength: 50, nullable: false),
                    firstname = table.Column<string>(maxLength: 50, nullable: false),
                    middlename = table.Column<string>(maxLength: 50, nullable: true),
                    extension = table.Column<string>(maxLength: 50, nullable: true),
                    birthday = table.Column<string>(nullable: true),
                    birthplace = table.Column<string>(maxLength: 50, nullable: true),
                    nationality = table.Column<string>(maxLength: 50, nullable: true),
                    civilstatus = table.Column<string>(maxLength: 50, nullable: true),
                    gender = table.Column<string>(maxLength: 6, nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Studentlevel",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentlevels = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studentlevel", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "StudentRelative",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(nullable: false),
                    fullname = table.Column<string>(maxLength: 150, nullable: false),
                    relation = table.Column<string>(maxLength: 50, nullable: false),
                    address = table.Column<string>(maxLength: 255, nullable: false),
                    occupation = table.Column<string>(maxLength: 50, nullable: true),
                    phonenumber = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRelative", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "StudentType",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studenttypes = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentType", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentlevel = table.Column<string>(maxLength: 50, nullable: false),
                    gradeyear = table.Column<string>(maxLength: 50, nullable: false),
                    category = table.Column<string>(maxLength: 100, nullable: false),
                    subjectcode = table.Column<string>(maxLength: 50, nullable: false),
                    subjectname = table.Column<string>(maxLength: 512, nullable: false),
                    semester = table.Column<string>(maxLength: 25, nullable: true),
                    noofhours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCategory",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCategory", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "ToPay",
                columns: table => new
                {
                    referenceno = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(nullable: false),
                    schoolyear = table.Column<string>(maxLength: 25, nullable: false),
                    billnames = table.Column<string>(maxLength: 50, nullable: false),
                    billdate = table.Column<DateTime>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    duedate = table.Column<DateTime>(nullable: false),
                    remarks = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToPay", x => x.referenceno);
                });

            migrationBuilder.CreateTable(
                name: "TrackCode",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentlevels = table.Column<string>(maxLength: 25, nullable: true),
                    trackcodes = table.Column<string>(maxLength: 25, nullable: true),
                    descriptions = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCode", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageAbout",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    description = table.Column<string>(nullable: true),
                    icon = table.Column<string>(maxLength: 50, nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageAbout", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageAchievement",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    achievementname = table.Column<string>(maxLength: 50, nullable: false),
                    icon = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true),
                    active = table.Column<bool>(nullable: false),
                    priority = table.Column<int>(nullable: false),
                    postdate = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageAchievement", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageBlog",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    blogtitle = table.Column<string>(maxLength: 50, nullable: false),
                    author = table.Column<string>(maxLength: 150, nullable: true),
                    postdate = table.Column<string>(maxLength: 150, nullable: true),
                    description = table.Column<string>(nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageBlog", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageContact",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(maxLength: 150, nullable: false),
                    address = table.Column<string>(maxLength: 512, nullable: false),
                    cellphoneno = table.Column<string>(maxLength: 25, nullable: false),
                    landlineno = table.Column<string>(maxLength: 25, nullable: false),
                    emailadd = table.Column<string>(maxLength: 150, nullable: false),
                    website = table.Column<string>(maxLength: 150, nullable: false),
                    longitude = table.Column<string>(maxLength: 150, nullable: false),
                    latitude = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageContact", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageCourse",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    category = table.Column<string>(maxLength: 50, nullable: false),
                    coursetitle = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(nullable: true),
                    icon = table.Column<string>(maxLength: 50, nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageCourse", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageCourseDetail",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    aboutthecourse = table.Column<string>(nullable: true),
                    requirement = table.Column<string>(nullable: true),
                    howtoenroll = table.Column<string>(nullable: true),
                    fees = table.Column<string>(nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    imagelink = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageCourseDetail", x => x.recordno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageEvent",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    eventtitle = table.Column<string>(maxLength: 50, nullable: false),
                    icon = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageEvent", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPages",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    subtitle = table.Column<string>(maxLength: 50, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: true),
                    description = table.Column<string>(nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    icon = table.Column<string>(maxLength: 150, nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPages", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageSubs",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    subtitle = table.Column<string>(maxLength: 50, nullable: false),
                    subsubtitle = table.Column<string>(maxLength: 150, nullable: true),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    description = table.Column<string>(nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    icon = table.Column<string>(maxLength: 150, nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    when = table.Column<string>(maxLength: 150, nullable: true),
                    personname = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageSubs", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageSuccessStory",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    subtitle = table.Column<string>(maxLength: 50, nullable: true),
                    storydate = table.Column<string>(maxLength: 150, nullable: true),
                    author = table.Column<string>(maxLength: 150, nullable: true),
                    paragraph1 = table.Column<string>(nullable: false),
                    image = table.Column<byte[]>(nullable: true),
                    icon = table.Column<string>(maxLength: 150, nullable: true),
                    link = table.Column<string>(maxLength: 150, nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageSuccessStory", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageTeacher",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 25, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: false),
                    category = table.Column<string>(maxLength: 50, nullable: false),
                    specialization = table.Column<string>(maxLength: 50, nullable: false),
                    fullname = table.Column<string>(maxLength: 50, nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageTeacher", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "WebPageTitles",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 150, nullable: false),
                    tagline = table.Column<string>(maxLength: 512, nullable: true),
                    description = table.Column<string>(nullable: true),
                    controller = table.Column<string>(maxLength: 150, nullable: true),
                    action = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    link = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageTitles", x => x.recno);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentImage",
                columns: table => new
                {
                    recno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(nullable: false),
                    link = table.Column<string>(maxLength: 150, nullable: false),
                    image = table.Column<byte[]>(nullable: true),
                    StudentInfoid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentImage", x => x.recno);
                    table.ForeignKey(
                        name: "FK_StudentImage_StudentInfo_StudentInfoid",
                        column: x => x.StudentInfoid,
                        principalTable: "StudentInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassVideo",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    subjectname = table.Column<string>(maxLength: 512, nullable: false),
                    lessonno = table.Column<int>(nullable: false),
                    lessontitle = table.Column<string>(maxLength: 150, nullable: false),
                    videolink = table.Column<string>(maxLength: 150, nullable: false),
                    active = table.Column<bool>(nullable: false),
                    Subjectrecno = table.Column<int>(nullable: true),
                    Subjectrecno1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassVideo", x => x.recordno);
                    table.ForeignKey(
                        name: "FK_ClassVideo_Subject_Subjectrecno",
                        column: x => x.Subjectrecno,
                        principalTable: "Subject",
                        principalColumn: "recno",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassVideo_Subject_Subjectrecno1",
                        column: x => x.Subjectrecno1,
                        principalTable: "Subject",
                        principalColumn: "recno",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebPageEventDetail",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    speakername = table.Column<string>(maxLength: 512, nullable: true),
                    eventdate = table.Column<string>(maxLength: 150, nullable: true),
                    venue = table.Column<string>(maxLength: 150, nullable: true),
                    eventdetails = table.Column<string>(nullable: true),
                    imagedetails = table.Column<byte[]>(nullable: true),
                    imagelink = table.Column<string>(maxLength: 1024, nullable: true),
                    WebPageEventrecno = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageEventDetail", x => x.recordno);
                    table.ForeignKey(
                        name: "FK_WebPageEventDetail_WebPageEvent_WebPageEventrecno",
                        column: x => x.WebPageEventrecno,
                        principalTable: "WebPageEvent",
                        principalColumn: "recno",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebPageTeacherDetail",
                columns: table => new
                {
                    recordno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recno = table.Column<int>(nullable: false),
                    jobdescription = table.Column<string>(maxLength: 1024, nullable: true),
                    biography = table.Column<string>(nullable: true),
                    hobbiesinterest = table.Column<string>(maxLength: 512, nullable: true),
                    contactinfo = table.Column<string>(maxLength: 512, nullable: true),
                    WebPageTeacherrecno = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageTeacherDetail", x => x.recordno);
                    table.ForeignKey(
                        name: "FK_WebPageTeacherDetail_WebPageTeacher_WebPageTeacherrecno",
                        column: x => x.WebPageTeacherrecno,
                        principalTable: "WebPageTeacher",
                        principalColumn: "recno",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassVideo_Subjectrecno",
                table: "ClassVideo",
                column: "Subjectrecno");

            migrationBuilder.CreateIndex(
                name: "IX_ClassVideo_Subjectrecno1",
                table: "ClassVideo",
                column: "Subjectrecno1");

            migrationBuilder.CreateIndex(
                name: "IX_StudentImage_StudentInfoid",
                table: "StudentImage",
                column: "StudentInfoid");

            migrationBuilder.CreateIndex(
                name: "IX_WebPageEventDetail_WebPageEventrecno",
                table: "WebPageEventDetail",
                column: "WebPageEventrecno");

            migrationBuilder.CreateIndex(
                name: "IX_WebPageTeacherDetail_WebPageTeacherrecno",
                table: "WebPageTeacherDetail",
                column: "WebPageTeacherrecno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetail");

            migrationBuilder.DropTable(
                name: "AdmissionRequirement");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BillName");

            migrationBuilder.DropTable(
                name: "BillToPay");

            migrationBuilder.DropTable(
                name: "CivilStatus");

            migrationBuilder.DropTable(
                name: "ClassAnnouncement");

            migrationBuilder.DropTable(
                name: "ClassLiveStream");

            migrationBuilder.DropTable(
                name: "ClassModule");

            migrationBuilder.DropTable(
                name: "ClassSchedule");

            migrationBuilder.DropTable(
                name: "ClassVideo");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "EnrollmentProdecure");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "FeeCategory");

            migrationBuilder.DropTable(
                name: "FeeDescription");

            migrationBuilder.DropTable(
                name: "FeeTable");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "GradeYear");

            migrationBuilder.DropTable(
                name: "ModeOfPayment");

            migrationBuilder.DropTable(
                name: "Nationality");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentOffice");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "ProcessIntro");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "SchoolYear");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "StatusOfPayment");

            migrationBuilder.DropTable(
                name: "StudentContact");

            migrationBuilder.DropTable(
                name: "StudentDocument");

            migrationBuilder.DropTable(
                name: "StudentImage");

            migrationBuilder.DropTable(
                name: "Studentlevel");

            migrationBuilder.DropTable(
                name: "StudentRelative");

            migrationBuilder.DropTable(
                name: "StudentType");

            migrationBuilder.DropTable(
                name: "SubjectCategory");

            migrationBuilder.DropTable(
                name: "ToPay");

            migrationBuilder.DropTable(
                name: "TrackCode");

            migrationBuilder.DropTable(
                name: "WebPageAbout");

            migrationBuilder.DropTable(
                name: "WebPageAchievement");

            migrationBuilder.DropTable(
                name: "WebPageBlog");

            migrationBuilder.DropTable(
                name: "WebPageContact");

            migrationBuilder.DropTable(
                name: "WebPageCourse");

            migrationBuilder.DropTable(
                name: "WebPageCourseDetail");

            migrationBuilder.DropTable(
                name: "WebPageEventDetail");

            migrationBuilder.DropTable(
                name: "WebPages");

            migrationBuilder.DropTable(
                name: "WebPageSubs");

            migrationBuilder.DropTable(
                name: "WebPageSuccessStory");

            migrationBuilder.DropTable(
                name: "WebPageTeacherDetail");

            migrationBuilder.DropTable(
                name: "WebPageTitles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "StudentInfo");

            migrationBuilder.DropTable(
                name: "WebPageEvent");

            migrationBuilder.DropTable(
                name: "WebPageTeacher");
        }
    }
}
