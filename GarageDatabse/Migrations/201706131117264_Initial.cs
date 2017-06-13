namespace GarageDatabse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonalNumber = c.String(nullable: false),
                        OwnerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegNumber = c.String(nullable: false, maxLength: 6),
                        ParkingSpotID = c.Int(nullable: false),
                        VehicleTypeID = c.Int(nullable: false),
                        OwnerID = c.Int(nullable: false),
                        ParkingPrice_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Owners", t => t.OwnerID, cascadeDelete: true)
                .ForeignKey("dbo.ParkingSpots", t => t.ParkingSpotID, cascadeDelete: true)
                .ForeignKey("dbo.ParkingPrices", t => t.ParkingPrice_ID)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeID, cascadeDelete: true)
                .Index(t => t.ParkingSpotID)
                .Index(t => t.VehicleTypeID)
                .Index(t => t.OwnerID)
                .Index(t => t.ParkingPrice_ID);
            
            CreateTable(
                "dbo.ParkingSpots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ParkingPlace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        ParkingPriceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ParkingPrices", t => t.ParkingPriceID, cascadeDelete: true)
                .Index(t => t.ParkingPriceID);
            
            CreateTable(
                "dbo.ParkingPrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParkingPrices = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateStoredProcedure(
                "dbo.Vehicle_Insert",
                p => new
                    {
                        RegNumber = p.String(maxLength: 6),
                        ParkingSpotID = p.Int(),
                        VehicleTypeID = p.Int(),
                        OwnerID = p.Int(),
                        ParkingPrice_ID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Vehicles]([RegNumber], [ParkingSpotID], [VehicleTypeID], [OwnerID], [ParkingPrice_ID])
                      VALUES (@RegNumber, @ParkingSpotID, @VehicleTypeID, @OwnerID, @ParkingPrice_ID)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Vehicles]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Vehicles] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Update",
                p => new
                    {
                        ID = p.Int(),
                        RegNumber = p.String(maxLength: 6),
                        ParkingSpotID = p.Int(),
                        VehicleTypeID = p.Int(),
                        OwnerID = p.Int(),
                        ParkingPrice_ID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Vehicles]
                      SET [RegNumber] = @RegNumber, [ParkingSpotID] = @ParkingSpotID, [VehicleTypeID] = @VehicleTypeID, [OwnerID] = @OwnerID, [ParkingPrice_ID] = @ParkingPrice_ID
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Vehicle_Delete",
                p => new
                    {
                        ID = p.Int(),
                        ParkingPrice_ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Vehicles]
                      WHERE (([ID] = @ID) AND (([ParkingPrice_ID] = @ParkingPrice_ID) OR ([ParkingPrice_ID] IS NULL AND @ParkingPrice_ID IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Vehicle_Delete");
            DropStoredProcedure("dbo.Vehicle_Update");
            DropStoredProcedure("dbo.Vehicle_Insert");
            DropForeignKey("dbo.Vehicles", "VehicleTypeID", "dbo.VehicleTypes");
            DropForeignKey("dbo.VehicleTypes", "ParkingPriceID", "dbo.ParkingPrices");
            DropForeignKey("dbo.Vehicles", "ParkingPrice_ID", "dbo.ParkingPrices");
            DropForeignKey("dbo.Vehicles", "ParkingSpotID", "dbo.ParkingSpots");
            DropForeignKey("dbo.Vehicles", "OwnerID", "dbo.Owners");
            DropIndex("dbo.VehicleTypes", new[] { "ParkingPriceID" });
            DropIndex("dbo.Vehicles", new[] { "ParkingPrice_ID" });
            DropIndex("dbo.Vehicles", new[] { "OwnerID" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeID" });
            DropIndex("dbo.Vehicles", new[] { "ParkingSpotID" });
            DropTable("dbo.ParkingPrices");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.ParkingSpots");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Owners");
        }
    }
}
