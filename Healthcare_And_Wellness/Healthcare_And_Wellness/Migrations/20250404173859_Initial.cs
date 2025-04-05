using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare_And_Wellness.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "GuidedActivities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidedActivities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "Injections",
                columns: table => new
                {
                    InjectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Limit = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injections", x => x.InjectionId);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    jobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.jobID);
                });

            migrationBuilder.CreateTable(
                name: "MentalArticles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentalArticles", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.WorkoutId);
                });

            migrationBuilder.CreateTable(
                name: "applicants",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jobID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicants", x => x.userID);
                    table.ForeignKey(
                        name: "FK_applicants_jobs_jobID",
                        column: x => x.jobID,
                        principalTable: "jobs",
                        principalColumn: "jobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BmiCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BmiResult = table.Column<double>(type: "float", nullable: true),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BmiCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BmiCalculations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRecommendations",
                columns: table => new
                {
                    RecommendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RecommendationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecommendations", x => x.RecommendationId);
                    table.ForeignKey(
                        name: "FK_HealthRecommendations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InjectionId = table.Column<int>(type: "int", nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Injections_InjectionId",
                        column: x => x.InjectionId,
                        principalTable: "Injections",
                        principalColumn: "InjectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfAssessments",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    TakenAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfAssessments", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_SelfAssessments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Steps = table.Column<int>(type: "int", nullable: false),
                    CaloriesBurned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_StepLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportPosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_SupportPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_SupportComments_SupportPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "SupportPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportReactions",
                columns: table => new
                {
                    ReactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportReactions", x => x.ReactionId);
                    table.ForeignKey(
                        name: "FK_SupportReactions_SupportPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "SupportPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportReports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportReports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_SupportReports_SupportPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "SupportPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "ServiceType", "Status", "UserId" },
                values: new object[] { 1, new DateTime(2025, 4, 6, 13, 38, 59, 41, DateTimeKind.Local).AddTicks(3667), "General Checkup", "Available", null });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "Content", "PublishedDate", "Title" },
                values: new object[] { 1, "Explore the newest research on mental health...", new DateTime(2025, 4, 4, 13, 38, 59, 41, DateTimeKind.Local).AddTicks(3631), "Latest in Mental Wellness" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "ConfirmPassword", "ContentType", "DateOfBirth", "Name", "Password", "ProfilePic", "Role", "Username" },
                values: new object[,]
                {
                    { 1, 30, "Admin@1234", null, "1994-01-01", "Administrator", "Admin@1234", null, "Admin", "admin" },
                    { 2, 21, "Lovepreet@1234", null, "2003-10-26", "Lovepreet Singh", "Lovepreet@1234", null, "Member", "member" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPlans",
                columns: new[] { "WorkoutId", "Category", "Description", "Duration", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "Strength", "A 45-minute strength training routine for beginners.", 45, "Full Body Strength Training", "https://www.youtube.com/watch?v=dQw4w9WgXcQ" },
                    { 2, "Yoga", "A 30-minute morning yoga session to start your day.", 30, "Morning Yoga Flow", "https://www.youtube.com/watch?v=v7AYKMP6rOE" }
                });

            migrationBuilder.InsertData(
                table: "jobs",
                columns: new[] { "jobID", "description", "jobName", "statusJob" },
                values: new object[] { 1, "The responsibility of the Instructor Therapist is to deliver direct ABA (Applied Behaviour Analysis) interventions. This includes providing input into the development and implementation of Behaviour Support Plans (BSPs), collecting baseline data, maintaining progress notes (i.e., case notes), the recording and graphing of relevant data, parent coaching, individual and group services, as well as the preparation of teaching materials and also working with, and mentoring volunteers.", "Instructor Therapist", "Apply" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "ServiceType", "Status", "UserId" },
                values: new object[] { 2, new DateTime(2025, 4, 7, 13, 38, 59, 41, DateTimeKind.Local).AddTicks(3670), "Dental Cleaning", "Pending", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_applicants_jobID",
                table: "applicants",
                column: "jobID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BmiCalculations_UserId",
                table: "BmiCalculations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecommendations_UserId",
                table: "HealthRecommendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_InjectionId",
                table: "Reservations",
                column: "InjectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfAssessments_UserId",
                table: "SelfAssessments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StepLogs_UserId",
                table: "StepLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportComments_PostId",
                table: "SupportComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportComments_UserId",
                table: "SupportComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportPosts_UserId",
                table: "SupportPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportReactions_PostId",
                table: "SupportReactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportReactions_UserId",
                table: "SupportReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportReports_PostId",
                table: "SupportReports",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportReports_UserId",
                table: "SupportReports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicants");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "BmiCalculations");

            migrationBuilder.DropTable(
                name: "GuidedActivities");

            migrationBuilder.DropTable(
                name: "HealthRecommendations");

            migrationBuilder.DropTable(
                name: "MentalArticles");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SelfAssessments");

            migrationBuilder.DropTable(
                name: "StepLogs");

            migrationBuilder.DropTable(
                name: "SupportComments");

            migrationBuilder.DropTable(
                name: "SupportReactions");

            migrationBuilder.DropTable(
                name: "SupportReports");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "Injections");

            migrationBuilder.DropTable(
                name: "SupportPosts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
