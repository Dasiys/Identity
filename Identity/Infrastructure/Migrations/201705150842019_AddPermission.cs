namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissionMap",
                c => new
                    {
                        PermissionId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.RoleId })
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolePermissionMap", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissionMap", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.RolePermissionMap", new[] { "RoleId" });
            DropIndex("dbo.RolePermissionMap", new[] { "PermissionId" });
            DropTable("dbo.RolePermissionMap");
            DropTable("dbo.Permissions");
        }
    }
}
