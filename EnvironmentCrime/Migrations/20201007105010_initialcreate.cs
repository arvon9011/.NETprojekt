﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnvironmentCrime.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<string>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    RoleTitle = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Errands",
                columns: table => new
                {
                    ErrandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNumber = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: false),
                    TypeOfCrime = table.Column<string>(nullable: false),
                    DateOfObservation = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    InvestigatorInfo = table.Column<string>(nullable: true),
                    InvestigatorAction = table.Column<string>(nullable: true),
                    InformerName = table.Column<string>(nullable: false),
                    InformerPhone = table.Column<string>(nullable: false),
                    StatusId = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errands", x => x.ErrandID);
                });

            migrationBuilder.CreateTable(
                name: "ErrandStatuses",
                columns: table => new
                {
                    StatusID = table.Column<string>(nullable: false),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrandStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Errands");

            migrationBuilder.DropTable(
                name: "ErrandStatuses");

            migrationBuilder.DropTable(
                name: "Sequences");
        }
    }
}
