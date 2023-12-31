Build started...
Build succeeded.
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201221032711_InitialCreate') THEN
    CREATE TABLE "PrinterDevices" (
        "Id" text NOT NULL,
        "DeviceName" text NULL,
        "PrinterName" text NULL,
        "RegisterDate" timestamp without time zone NULL,
        "ShutdownDate" timestamp without time zone NULL,
        "IsActive" boolean NOT NULL,
        CONSTRAINT "PK_PrinterDevices" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201221032711_InitialCreate') THEN
    CREATE TABLE "PrinterJobExecuteds" (
        "Id" text NOT NULL,
        "FilePath" text NULL,
        "PrinterName" text NULL,
        "PrinterSetting" text NULL,
        "CreatedDate" timestamp without time zone NULL,
        "PrinterType" text NULL,
        "GoogleCloudSetting" text NULL,
        "ContractId" bigint NOT NULL,
        CONSTRAINT "PK_PrinterJobExecuteds" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201221032711_InitialCreate') THEN
    CREATE TABLE "PrinterJobFaileds" (
        "Id" text NOT NULL,
        "FilePath" text NULL,
        "PrinterName" text NULL,
        "PrinterSetting" text NULL,
        "CreatedDate" timestamp without time zone NULL,
        "ErrorMessage" text NULL,
        "PrinterType" text NULL,
        "GoogleCloudSetting" text NULL,
        "ContractId" bigint NOT NULL,
        CONSTRAINT "PK_PrinterJobFaileds" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201221032711_InitialCreate') THEN
    CREATE TABLE "PrinterJobPendings" (
        "Id" text NOT NULL,
        "FilePath" text NULL,
        "PrinterName" text NULL,
        "PrinterSetting" text NULL,
        "CreatedDate" timestamp without time zone NULL,
        "PrinterType" text NULL,
        "GoogleCloudSetting" text NULL,
        "ContractId" bigint NOT NULL,
        "PrinterProcessingId" text NULL,
        CONSTRAINT "PK_PrinterJobPendings" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201221032711_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201221032711_InitialCreate', '2.2.6-servicing-10079');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobPendings" ADD "FromPage" integer NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobPendings" ADD "IsDuplex" boolean NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobPendings" ADD "IsHorizontal" boolean NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobPendings" ADD "ToPage" integer NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobFaileds" ADD "FromPage" integer NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobFaileds" ADD "IsDuplex" boolean NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobFaileds" ADD "IsHorizontal" boolean NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobFaileds" ADD "ToPage" integer NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobExecuteds" ADD "FromPage" integer NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobExecuteds" ADD "IsDuplex" boolean NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobExecuteds" ADD "IsHorizontal" boolean NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    ALTER TABLE "PrinterJobExecuteds" ADD "ToPage" integer NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210117161651_UpdatePrinterDetail') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210117161651_UpdatePrinterDetail', '2.2.6-servicing-10079');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210122065801_AddParamFromPage') THEN
    ALTER TABLE "PrinterJobPendings" ADD "FileType" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210122065801_AddParamFromPage') THEN
    ALTER TABLE "PrinterJobFaileds" ADD "FileType" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210122065801_AddParamFromPage') THEN
    ALTER TABLE "PrinterJobExecuteds" ADD "FileType" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210122065801_AddParamFromPage') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210122065801_AddParamFromPage', '2.2.6-servicing-10079');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210123072620_AddPrinterDeviceName') THEN
    ALTER TABLE "PrinterJobPendings" ADD "PrinterDeviceName" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210123072620_AddPrinterDeviceName') THEN
    ALTER TABLE "PrinterJobFaileds" ADD "PrinterDeviceName" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210123072620_AddPrinterDeviceName') THEN
    ALTER TABLE "PrinterJobExecuteds" ADD "PrinterDeviceName" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210123072620_AddPrinterDeviceName') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210123072620_AddPrinterDeviceName', '2.2.6-servicing-10079');
    END IF;
END $$;

