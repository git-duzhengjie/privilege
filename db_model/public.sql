/*
 Navicat Premium Data Transfer

 Source Server         : 234
 Source Server Type    : PostgreSQL
 Source Server Version : 100005
 Source Host           : 192.168.0.234:5432
 Source Catalog        : orApr
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 100005
 File Encoding         : 65001

 Date: 19/07/2019 14:58:27
*/


-- ----------------------------
-- Sequence structure for users_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."users_id_seq";
CREATE SEQUENCE "public"."users_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Table structure for pm_attribution_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_attribution_type";
CREATE TABLE "public"."pm_attribution_type" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "ChineseName" varchar(255) COLLATE "pg_catalog"."default",
  "WidgetType" int2,
  "WidgetTip" varchar(255) COLLATE "pg_catalog"."default",
  "IsRequired" bool,
  "ValidVerifyType" int2,
  "OptionItems" text COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6) DEFAULT now(),
  "OrganizationTypeId" char(32) COLLATE "pg_catalog"."default",
  "IsBase" bool,
  "IsSearch" bool,
  "EnglishName" varchar(255) COLLATE "pg_catalog"."default",
  "ApiName" varchar(255) COLLATE "pg_catalog"."default",
  "UpdateTime" timestamp(6),
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_item
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_item";
CREATE TABLE "public"."pm_item" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "SystemJsonItem" jsonb,
  "CreateTime" timestamp(6),
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "SystemId" char(32) COLLATE "pg_catalog"."default",
  "Status" bool,
  "UpdateTime" timestamp(6),
  "FrontSystemCode" varchar(32) COLLATE "pg_catalog"."default",
  "FrontSystemName" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_item_content
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_item_content";
CREATE TABLE "public"."pm_item_content" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Path" varchar(255) COLLATE "pg_catalog"."default",
  "Title" varchar(255) COLLATE "pg_catalog"."default",
  "Icon" varchar(255) COLLATE "pg_catalog"."default",
  "Status" bool,
  "ParentId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6),
  "UpdateTime" timestamp(6),
  "IsHasChildren" bool,
  "ItemId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_organization
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_organization";
CREATE TABLE "public"."pm_organization" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Code" varchar(255) COLLATE "pg_catalog"."default",
  "ParentId" char(32) COLLATE "pg_catalog"."default" DEFAULT '-1'::integer,
  "ParentCode" varchar(255) COLLATE "pg_catalog"."default",
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "TypeId" char(32) COLLATE "pg_catalog"."default",
  "HierarchyType" int2,
  "IsHasChildren" bool,
  "CreateTime" timestamp(6) DEFAULT now(),
  "ExtendAttribution" jsonb,
  "UpdateTime" timestamp(6),
  "State" bool,
  "Instruction" text COLLATE "pg_catalog"."default",
  "IsHasChildOrganization" bool,
  "IsArea" bool,
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_organization_type
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_organization_type";
CREATE TABLE "public"."pm_organization_type" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Code" varchar(5) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "SystemId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6) DEFAULT now(),
  "Instruction" text COLLATE "pg_catalog"."default",
  "IsRelevancy" bool DEFAULT false,
  "UpdateTime" timestamp(6),
  "IsHasChildren" bool,
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_privilege
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_privilege";
CREATE TABLE "public"."pm_privilege" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "GroupId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(0) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "Instruction" text COLLATE "pg_catalog"."default",
  "Code" varchar(32) COLLATE "pg_catalog"."default",
  "CloneId" char(32) COLLATE "pg_catalog"."default",
  "OriginalCode" varchar(32) COLLATE "pg_catalog"."default"
)
;
COMMENT ON COLUMN "public"."pm_privilege"."Id" IS '权限ID';
COMMENT ON COLUMN "public"."pm_privilege"."Name" IS '权限名';
COMMENT ON COLUMN "public"."pm_privilege"."GroupId" IS '权限所属组ID';
COMMENT ON COLUMN "public"."pm_privilege"."CreateTime" IS '权限创建时间';

-- ----------------------------
-- Table structure for pm_privilege_group
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_privilege_group";
CREATE TABLE "public"."pm_privilege_group" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "SystemId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(0) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;
COMMENT ON COLUMN "public"."pm_privilege_group"."Name" IS '权限组名';
COMMENT ON COLUMN "public"."pm_privilege_group"."CreateTime" IS '创建时间';

-- ----------------------------
-- Table structure for pm_relation_organization
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_organization";
CREATE TABLE "public"."pm_relation_organization" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "OrganizationId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "RelationOrganizationId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "OrganizationCode" varchar(255) COLLATE "pg_catalog"."default",
  "RelationOrganizationCode" varchar(255) COLLATE "pg_catalog"."default",
  "RelationAreaId" char(32) COLLATE "pg_catalog"."default",
  "RelationAreaCode" varchar(255) COLLATE "pg_catalog"."default",
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_relation_organization_foreign
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_organization_foreign";
CREATE TABLE "public"."pm_relation_organization_foreign" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "OrganizationId" char(32) COLLATE "pg_catalog"."default",
  "UnionId" char(32) COLLATE "pg_catalog"."default",
  "UpdateTime" timestamp(6),
  "CreateTime" timestamp(6) DEFAULT CURRENT_TIMESTAMP,
  "UnionName" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_relation_position_role
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_position_role";
CREATE TABLE "public"."pm_relation_position_role" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "PositionId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "CloneId" char(32) COLLATE "pg_catalog"."default",
  "PositionCode" varchar(255) COLLATE "pg_catalog"."default",
  "RoleCode" varchar(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_relation_role_item
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_role_item";
CREATE TABLE "public"."pm_relation_role_item" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleId" char(32) COLLATE "pg_catalog"."default",
  "ItemId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6),
  "UpdateTime" timestamp(6)
)
;

-- ----------------------------
-- Table structure for pm_relation_role_privilege
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_role_privilege";
CREATE TABLE "public"."pm_relation_role_privilege" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "PrivilegeId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(0) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "PrivilegeCode" varchar(32) COLLATE "pg_catalog"."default",
  "CloneId" char(32) COLLATE "pg_catalog"."default",
  "RoleCode" varchar(32) COLLATE "pg_catalog"."default"
)
;
COMMENT ON COLUMN "public"."pm_relation_role_privilege"."RoleId" IS '角色ID';
COMMENT ON COLUMN "public"."pm_relation_role_privilege"."PrivilegeId" IS '权限ID';
COMMENT ON COLUMN "public"."pm_relation_role_privilege"."CreateTime" IS '创建时间';

-- ----------------------------
-- Table structure for pm_relation_user_organization
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_user_organization";
CREATE TABLE "public"."pm_relation_user_organization" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "OrganizationId" char(32) COLLATE "pg_catalog"."default",
  "UserId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) DEFAULT now(),
  "OrganizationIdO" char(32) COLLATE "pg_catalog"."default",
  "DepartmentId" char(32) COLLATE "pg_catalog"."default",
  "PositionId" char(32) COLLATE "pg_catalog"."default",
  "UpdateTime" timestamp(6),
  "OrganizationCode" varchar(255) COLLATE "pg_catalog"."default",
  "DepartmentCode" varchar(255) COLLATE "pg_catalog"."default",
  "PositionCode" varchar(255) COLLATE "pg_catalog"."default",
  "UserType" int2,
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_relation_user_privilege
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_user_privilege";
CREATE TABLE "public"."pm_relation_user_privilege" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "UserId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "PrivilegeId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "PrivilegeCode" varchar(32) COLLATE "pg_catalog"."default",
  "CloneId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_relation_user_role
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_relation_user_role";
CREATE TABLE "public"."pm_relation_user_role" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "UserId" char(32) COLLATE "pg_catalog"."default",
  "RoleId" char(32) COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "CloneId" char(32) COLLATE "pg_catalog"."default",
  "RoleCode" varchar(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_role
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_role";
CREATE TABLE "public"."pm_role" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "State" bool,
  "SystemId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateTime" timestamp(6) DEFAULT now(),
  "UpdateTime" timestamp(6),
  "CloneId" char(32) COLLATE "pg_catalog"."default",
  "Instruction" varchar(255) COLLATE "pg_catalog"."default",
  "JsonItem" json,
  "Code" varchar(32) COLLATE "pg_catalog"."default",
  "OriginalCode" varchar(32) COLLATE "pg_catalog"."default",
  "ItemId" char(32) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for pm_system
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_system";
CREATE TABLE "public"."pm_system" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Code" varchar(2) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "EnglishName" varchar(255) COLLATE "pg_catalog"."default",
  "Instruction" text COLLATE "pg_catalog"."default",
  "LogoUrl" text COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(0) DEFAULT now(),
  "UpdateTime" timestamp(6)
)
;

-- ----------------------------
-- Table structure for pm_user
-- ----------------------------
DROP TABLE IF EXISTS "public"."pm_user";
CREATE TABLE "public"."pm_user" (
  "Id" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "OpenId" varchar(255) COLLATE "pg_catalog"."default",
  "UnionId" varchar(255) COLLATE "pg_catalog"."default",
  "Account" varchar(255) COLLATE "pg_catalog"."default",
  "Password" varchar(255) COLLATE "pg_catalog"."default",
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "Telephone" varchar(255) COLLATE "pg_catalog"."default",
  "Email" varchar(255) COLLATE "pg_catalog"."default",
  "State" bool,
  "Instruction" text COLLATE "pg_catalog"."default",
  "CreateTime" timestamp(6) DEFAULT now(),
  "Channel" int2,
  "ExtendAttribution" json,
  "LastLoginTime" timestamp(6),
  "PortraitUrl" text COLLATE "pg_catalog"."default",
  "UpdateTime" timestamp(6),
  "SystemId" char(32) COLLATE "pg_catalog"."default" NOT NULL,
  "SystemCode" char(2) COLLATE "pg_catalog"."default",
  "CloneId" char(32) COLLATE "pg_catalog"."default" DEFAULT NULL::bpchar,
  "SessionKey" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for test
-- ----------------------------
DROP TABLE IF EXISTS "public"."test";
CREATE TABLE "public"."test" (
  "test" json,
  "t" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for test_dapper_sql
-- ----------------------------
DROP TABLE IF EXISTS "public"."test_dapper_sql";
CREATE TABLE "public"."test_dapper_sql" (
  "id" varchar(255) COLLATE "pg_catalog"."default",
  "value" varchar(255) COLLATE "pg_catalog"."default",
  "value2" varchar(255) COLLATE "pg_catalog"."default",
  "value3" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for tu_unit
-- ----------------------------
DROP TABLE IF EXISTS "public"."tu_unit";
CREATE TABLE "public"."tu_unit" (
  "Id" varchar(255) COLLATE "pg_catalog"."default",
  "ContractName2" varchar(255) COLLATE "pg_catalog"."default",
  "ContractMobile2" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for tu_user
-- ----------------------------
DROP TABLE IF EXISTS "public"."tu_user";
CREATE TABLE "public"."tu_user" (
  "Id" varchar(255) COLLATE "pg_catalog"."default",
  "Name" varchar(255) COLLATE "pg_catalog"."default",
  "Account" varchar(255) COLLATE "pg_catalog"."default",
  "Password" varchar(255) COLLATE "pg_catalog"."default",
  "Mobile" varchar(255) COLLATE "pg_catalog"."default",
  "Sex" varchar(255) COLLATE "pg_catalog"."default",
  "SpareName" varchar(255) COLLATE "pg_catalog"."default",
  "SpareMobile" varchar(255) COLLATE "pg_catalog"."default",
  "CreateTime" varchar(255) COLLATE "pg_catalog"."default",
  "CreatorId" varchar(255) COLLATE "pg_catalog"."default",
  "UnitId" varchar(255) COLLATE "pg_catalog"."default",
  "Role" varchar(255) COLLATE "pg_catalog"."default",
  "HeadPic" varchar(255) COLLATE "pg_catalog"."default",
  "Status" varchar(255) COLLATE "pg_catalog"."default",
  "IsCompany" varchar(255) COLLATE "pg_catalog"."default",
  "NickName" varchar(255) COLLATE "pg_catalog"."default",
  "Email" varchar(255) COLLATE "pg_catalog"."default",
  "SourceType" varchar(255) COLLATE "pg_catalog"."default",
  "Birthday" varchar(255) COLLATE "pg_catalog"."default",
  "StoreCredit" varchar(255) COLLATE "pg_catalog"."default",
  "Vip" varchar(255) COLLATE "pg_catalog"."default",
  "HxUserId" varchar(255) COLLATE "pg_catalog"."default",
  "Introduction" varchar(255) COLLATE "pg_catalog"."default",
  "Location" varchar(255) COLLATE "pg_catalog"."default",
  "Vehicle" varchar(255) COLLATE "pg_catalog"."default",
  "VehicleName" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for tu_useridentify
-- ----------------------------
DROP TABLE IF EXISTS "public"."tu_useridentify";
CREATE TABLE "public"."tu_useridentify" (
  "UserId" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  "Type" int4 NOT NULL,
  "Number" varchar(128) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Function structure for json_object_del_key
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."json_object_del_key"("json" jsonb, "key_to_del" text);
CREATE OR REPLACE FUNCTION "public"."json_object_del_key"("json" jsonb, "key_to_del" text)
  RETURNS "pg_catalog"."jsonb" AS $BODY$
SELECT CASE
  WHEN ("json" -> "key_to_del") IS NULL THEN "json"
  ELSE (SELECT concat('{', string_agg(to_json("key") || ':' || "value", ','), '}')
          FROM (SELECT *
                  FROM jsonb_each("json")
                 WHERE "key" <> "key_to_del"
               ) AS "fields")::jsonb
END
$BODY$
  LANGUAGE sql IMMUTABLE STRICT
  COST 100;

-- ----------------------------
-- Function structure for json_object_del_path
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."json_object_del_path"("json" jsonb, "key_path" _text);
CREATE OR REPLACE FUNCTION "public"."json_object_del_path"("json" jsonb, "key_path" _text)
  RETURNS "pg_catalog"."jsonb" AS $BODY$
SELECT CASE
  WHEN ("json" -> "key_path"[l] ) IS NULL THEN "json"
  ELSE
     CASE COALESCE(array_length("key_path", 1), 0)
         WHEN 0 THEN "json"
         WHEN 1 THEN "json_object_del_key"("json", "key_path"[l])
         ELSE "json_object_set_key"(
           "json",
           "key_path"[l],
           "json_object_del_path"(
             COALESCE(NULLIF(("json" -> "key_path"[l])::text, 'null'), '{}')::jsonb,
             "key_path"[l+1:u]
           )
         )
       END
    END
  FROM array_lower("key_path", 1) l,
       array_upper("key_path", 1) u
$BODY$
  LANGUAGE sql IMMUTABLE STRICT
  COST 100;

-- ----------------------------
-- Function structure for json_object_set_key
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."json_object_set_key"("json" jsonb, "key_to_set" text, "value_to_set" anyelement);
CREATE OR REPLACE FUNCTION "public"."json_object_set_key"("json" jsonb, "key_to_set" text, "value_to_set" anyelement)
  RETURNS "pg_catalog"."jsonb" AS $BODY$
SELECT concat('{', string_agg(to_json("key") || ':' || "value", ','), '}')::jsonb
  FROM (SELECT *
          FROM jsonb_each("json")
         WHERE "key" <> "key_to_set"
         UNION ALL
        SELECT "key_to_set", to_jsonb("value_to_set")) AS "fields"
$BODY$
  LANGUAGE sql IMMUTABLE STRICT
  COST 100;

-- ----------------------------
-- Function structure for json_object_set_path
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."json_object_set_path"("json" json, "key_path" _text, "value_to_set" anyelement);
CREATE OR REPLACE FUNCTION "public"."json_object_set_path"("json" json, "key_path" _text, "value_to_set" anyelement)
  RETURNS "pg_catalog"."json" AS $BODY$
SELECT CASE COALESCE(array_length("key_path", 1), 0)
         WHEN 0 THEN to_json("value_to_set")
         WHEN 1 THEN "json_object_set_key"("json", "key_path"[l], "value_to_set")
         ELSE "json_object_set_key"(
           "json",
           "key_path"[l],
           "json_object_set_path"(
             COALESCE(NULLIF(("json" -> "key_path"[l])::text, 'null'), '{}')::json,
             "key_path"[l+1:u],
             "value_to_set"
           )
         )
       END
  FROM array_lower("key_path", 1) l,
       array_upper("key_path", 1) u
$BODY$
  LANGUAGE sql IMMUTABLE STRICT
  COST 100;

-- ----------------------------
-- Function structure for jsonb_append
-- ----------------------------
DROP FUNCTION IF EXISTS "public"."jsonb_append"("js" jsonb, "jsapp" jsonb, _text);
CREATE OR REPLACE FUNCTION "public"."jsonb_append"("js" jsonb, "jsapp" jsonb, _text)
  RETURNS "pg_catalog"."jsonb" AS $BODY$  
declare  
  x text;  
  sql text := format('%L', js);  
  tmp jsonb;  
  res jsonb;  
begin  
  foreach x in array $3 loop  
    sql := format ('jsonb_extract_path(%s, %L)', sql, x) ;  
    -- raise notice '%', sql;  
  end loop;  
  EXECUTE format('select jsonb_concat(%s, %L)', sql, jsapp) INTO tmp;  
  res := jsonb_set(js, $3, tmp);  
  return res;  
end;  
$BODY$
  LANGUAGE plpgsql VOLATILE STRICT
  COST 100;

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
SELECT setval('"public"."users_id_seq"', 2, false);

-- ----------------------------
-- Uniques structure for table pm_item
-- ----------------------------
ALTER TABLE "public"."pm_item" ADD CONSTRAINT "un_item" UNIQUE ("Name", "SystemId");

-- ----------------------------
-- Primary Key structure for table pm_item
-- ----------------------------
ALTER TABLE "public"."pm_item" ADD CONSTRAINT "pm_item_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table pm_item_content
-- ----------------------------
ALTER TABLE "public"."pm_item_content" ADD CONSTRAINT "pm_item_content_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_organization
-- ----------------------------
CREATE INDEX "inde_na" ON "public"."pm_organization" USING btree (
  "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_tp" ON "public"."pm_organization" USING btree (
  "TypeId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_organization
-- ----------------------------
ALTER TABLE "public"."pm_organization" ADD CONSTRAINT "un_or_na" UNIQUE ("ParentId", "Name");
ALTER TABLE "public"."pm_organization" ADD CONSTRAINT "un_co" UNIQUE ("Code");

-- ----------------------------
-- Primary Key structure for table pm_organization
-- ----------------------------
ALTER TABLE "public"."pm_organization" ADD CONSTRAINT "pm_organization_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_organization_type
-- ----------------------------
CREATE INDEX "index_s" ON "public"."pm_organization_type" USING btree (
  "SystemId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_organization_type
-- ----------------------------
ALTER TABLE "public"."pm_organization_type" ADD CONSTRAINT "un_orp_na" UNIQUE ("Name", "SystemId");
ALTER TABLE "public"."pm_organization_type" ADD CONSTRAINT "un_co_op" UNIQUE ("Code");

-- ----------------------------
-- Primary Key structure for table pm_organization_type
-- ----------------------------
ALTER TABLE "public"."pm_organization_type" ADD CONSTRAINT "pm_organization_type_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_privilege
-- ----------------------------
CREATE INDEX "index_pr_na" ON "public"."pm_privilege" USING btree (
  "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_privilege
-- ----------------------------
ALTER TABLE "public"."pm_privilege" ADD CONSTRAINT "un_pr_na" UNIQUE ("Name", "GroupId");
ALTER TABLE "public"."pm_privilege" ADD CONSTRAINT "un_pr_co" UNIQUE ("Code");

-- ----------------------------
-- Primary Key structure for table pm_privilege
-- ----------------------------
ALTER TABLE "public"."pm_privilege" ADD CONSTRAINT "pm_privilege_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_privilege_group
-- ----------------------------
CREATE INDEX "index_pg_name" ON "public"."pm_privilege_group" USING btree (
  "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_privilege_group
-- ----------------------------
ALTER TABLE "public"."pm_privilege_group" ADD CONSTRAINT "un_pr_gr_na" UNIQUE ("Name", "SystemId");

-- ----------------------------
-- Primary Key structure for table pm_privilege_group
-- ----------------------------
ALTER TABLE "public"."pm_privilege_group" ADD CONSTRAINT "pm_privilege_group_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_relation_organization
-- ----------------------------
CREATE INDEX "index_pr" ON "public"."pm_relation_organization" USING btree (
  "OrganizationId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);
CREATE INDEX "index_sb" ON "public"."pm_relation_organization" USING btree (
  "RelationOrganizationId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_relation_organization
-- ----------------------------
ALTER TABLE "public"."pm_relation_organization" ADD CONSTRAINT "un_rsb" UNIQUE ("OrganizationId", "RelationAreaId", "RelationOrganizationId");
ALTER TABLE "public"."pm_relation_organization" ADD CONSTRAINT "un_ror" UNIQUE ("OrganizationId", "RelationOrganizationId");

-- ----------------------------
-- Primary Key structure for table pm_relation_organization
-- ----------------------------
ALTER TABLE "public"."pm_relation_organization" ADD CONSTRAINT "pm_relation_organization_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Uniques structure for table pm_relation_position_role
-- ----------------------------
ALTER TABLE "public"."pm_relation_position_role" ADD CONSTRAINT "un_rpr" UNIQUE ("PositionId", "RoleId");

-- ----------------------------
-- Primary Key structure for table pm_relation_position_role
-- ----------------------------
ALTER TABLE "public"."pm_relation_position_role" ADD CONSTRAINT "pm_relation_position_role_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_relation_role_privilege
-- ----------------------------
CREATE INDEX "index_rl" ON "public"."pm_relation_role_privilege" USING btree (
  "RoleId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);
CREATE UNIQUE INDEX "index_un" ON "public"."pm_relation_role_privilege" USING btree (
  "RoleId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST,
  "PrivilegeId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_relation_role_privilege
-- ----------------------------
ALTER TABLE "public"."pm_relation_role_privilege" ADD CONSTRAINT "un_rrp" UNIQUE ("RoleId", "PrivilegeId");

-- ----------------------------
-- Primary Key structure for table pm_relation_role_privilege
-- ----------------------------
ALTER TABLE "public"."pm_relation_role_privilege" ADD CONSTRAINT "pm_relation_role_privilege_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_relation_user_organization
-- ----------------------------
CREATE INDEX "index_r_dp" ON "public"."pm_relation_user_organization" USING btree (
  "DepartmentId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);
CREATE INDEX "index_r_dpo" ON "public"."pm_relation_user_organization" USING btree (
  "DepartmentCode" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_r_op" ON "public"."pm_relation_user_organization" USING btree (
  "OrganizationId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);
CREATE INDEX "index_r_opo" ON "public"."pm_relation_user_organization" USING btree (
  "OrganizationCode" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_r_po" ON "public"."pm_relation_user_organization" USING btree (
  "PositionCode" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_r_pp" ON "public"."pm_relation_user_organization" USING btree (
  "PositionId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);
CREATE INDEX "index_r_us" ON "public"."pm_relation_user_organization" USING btree (
  "UserId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_relation_user_organization
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "un_rl_uo" UNIQUE ("OrganizationId", "UserId", "DepartmentId", "PositionId");

-- ----------------------------
-- Primary Key structure for table pm_relation_user_organization
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "pm_relation_user_organization_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Uniques structure for table pm_relation_user_privilege
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_privilege" ADD CONSTRAINT "un_rup" UNIQUE ("UserId", "PrivilegeId");

-- ----------------------------
-- Primary Key structure for table pm_relation_user_privilege
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_privilege" ADD CONSTRAINT "pm_relation_user_privilege_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_relation_user_role
-- ----------------------------
CREATE INDEX "index_rur_rd" ON "public"."pm_relation_user_role" USING btree (
  "RoleId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);
CREATE INDEX "index_rur_ro" ON "public"."pm_relation_user_role" USING btree (
  "RoleCode" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_rur_us" ON "public"."pm_relation_user_role" USING btree (
  "UserId" COLLATE "pg_catalog"."default" "pg_catalog"."bpchar_ops" ASC NULLS LAST
);

-- ----------------------------
-- Indexes structure for table pm_role
-- ----------------------------
CREATE INDEX "index_na" ON "public"."pm_role" USING btree (
  "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_ro_co" ON "public"."pm_role" USING btree (
  "Code" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_role
-- ----------------------------
ALTER TABLE "public"."pm_role" ADD CONSTRAINT "un_ro_na" UNIQUE ("Name", "SystemId");
ALTER TABLE "public"."pm_role" ADD CONSTRAINT "un_ro_co" UNIQUE ("Code");

-- ----------------------------
-- Primary Key structure for table pm_role
-- ----------------------------
ALTER TABLE "public"."pm_role" ADD CONSTRAINT "pm_role_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_system
-- ----------------------------
CREATE INDEX "index_sy_na" ON "public"."pm_system" USING btree (
  "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_system
-- ----------------------------
ALTER TABLE "public"."pm_system" ADD CONSTRAINT "un_sys_na" UNIQUE ("Name");
ALTER TABLE "public"."pm_system" ADD CONSTRAINT "un_sys_en" UNIQUE ("EnglishName");
ALTER TABLE "public"."pm_system" ADD CONSTRAINT "un_co_sy" UNIQUE ("Code");

-- ----------------------------
-- Primary Key structure for table pm_system
-- ----------------------------
ALTER TABLE "public"."pm_system" ADD CONSTRAINT "pm_system_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table pm_user
-- ----------------------------
CREATE INDEX "index_cr" ON "public"."pm_user" USING btree (
  "CreateTime" "pg_catalog"."timestamp_ops" ASC NULLS LAST
);
CREATE INDEX "index_op" ON "public"."pm_user" USING btree (
  "OpenId" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);
CREATE INDEX "index_us_na" ON "public"."pm_user" USING btree (
  "Name" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Uniques structure for table pm_user
-- ----------------------------
ALTER TABLE "public"."pm_user" ADD CONSTRAINT "un_opd" UNIQUE ("OpenId", "SystemId");
ALTER TABLE "public"."pm_user" ADD CONSTRAINT "un_und" UNIQUE ("UnionId", "SystemId");

-- ----------------------------
-- Primary Key structure for table pm_user
-- ----------------------------
ALTER TABLE "public"."pm_user" ADD CONSTRAINT "pm_user_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Indexes structure for table tu_useridentify
-- ----------------------------
CREATE INDEX "index_ud" ON "public"."tu_useridentify" USING btree (
  "UserId" COLLATE "pg_catalog"."default" "pg_catalog"."text_ops" ASC NULLS LAST
);

-- ----------------------------
-- Foreign Keys structure for table pm_attribution_type
-- ----------------------------
ALTER TABLE "public"."pm_attribution_type" ADD CONSTRAINT "fk_type" FOREIGN KEY ("OrganizationTypeId") REFERENCES "public"."pm_organization_type" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_item_content
-- ----------------------------
ALTER TABLE "public"."pm_item_content" ADD CONSTRAINT "fk_it_it" FOREIGN KEY ("ItemId") REFERENCES "public"."pm_item" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."pm_item_content" ADD CONSTRAINT "fk_it_pa" FOREIGN KEY ("ParentId") REFERENCES "public"."pm_item_content" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_organization
-- ----------------------------
ALTER TABLE "public"."pm_organization" ADD CONSTRAINT "fk_pd" FOREIGN KEY ("ParentId") REFERENCES "public"."pm_organization" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."pm_organization" ADD CONSTRAINT "fk_type_id" FOREIGN KEY ("TypeId") REFERENCES "public"."pm_organization_type" ("Id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_organization_type
-- ----------------------------
ALTER TABLE "public"."pm_organization_type" ADD CONSTRAINT "fk_ot_sy" FOREIGN KEY ("SystemId") REFERENCES "public"."pm_system" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_privilege
-- ----------------------------
ALTER TABLE "public"."pm_privilege" ADD CONSTRAINT "fk_privilege" FOREIGN KEY ("GroupId") REFERENCES "public"."pm_privilege_group" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_privilege_group
-- ----------------------------
ALTER TABLE "public"."pm_privilege_group" ADD CONSTRAINT "fk_system_id" FOREIGN KEY ("SystemId") REFERENCES "public"."pm_system" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_organization
-- ----------------------------
ALTER TABLE "public"."pm_relation_organization" ADD CONSTRAINT "rl_or" FOREIGN KEY ("OrganizationId") REFERENCES "public"."pm_organization" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."pm_relation_organization" ADD CONSTRAINT "rl_ra" FOREIGN KEY ("RelationAreaId") REFERENCES "public"."pm_organization" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_organization" ADD CONSTRAINT "rl_ro" FOREIGN KEY ("RelationOrganizationId") REFERENCES "public"."pm_organization" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_organization_foreign
-- ----------------------------
ALTER TABLE "public"."pm_relation_organization_foreign" ADD CONSTRAINT "fk_or_fr" FOREIGN KEY ("OrganizationId") REFERENCES "public"."pm_organization" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_position_role
-- ----------------------------
ALTER TABLE "public"."pm_relation_position_role" ADD CONSTRAINT "fk_rp_pd" FOREIGN KEY ("PositionId") REFERENCES "public"."pm_organization" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."pm_relation_position_role" ADD CONSTRAINT "fk_rp_rd" FOREIGN KEY ("RoleId") REFERENCES "public"."pm_role" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_role_item
-- ----------------------------
ALTER TABLE "public"."pm_relation_role_item" ADD CONSTRAINT "fk__ri_rd" FOREIGN KEY ("RoleId") REFERENCES "public"."pm_role" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."pm_relation_role_item" ADD CONSTRAINT "fk_ri_id" FOREIGN KEY ("ItemId") REFERENCES "public"."pm_item" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_role_privilege
-- ----------------------------
ALTER TABLE "public"."pm_relation_role_privilege" ADD CONSTRAINT "fk_rl_pv" FOREIGN KEY ("PrivilegeId") REFERENCES "public"."pm_privilege" ("Id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."pm_relation_role_privilege" ADD CONSTRAINT "fk_rl_rl" FOREIGN KEY ("RoleId") REFERENCES "public"."pm_role" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_user_organization
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "fk_ps_de" FOREIGN KEY ("DepartmentId") REFERENCES "public"."pm_organization" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "fk_ps_or" FOREIGN KEY ("OrganizationIdO") REFERENCES "public"."pm_organization" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "fk_ps_oro" FOREIGN KEY ("OrganizationId") REFERENCES "public"."pm_organization" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "fk_ps_po" FOREIGN KEY ("PositionId") REFERENCES "public"."pm_organization" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_user_organization" ADD CONSTRAINT "fk_ps_us" FOREIGN KEY ("UserId") REFERENCES "public"."pm_user" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_user_privilege
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_privilege" ADD CONSTRAINT "fk_up_pr" FOREIGN KEY ("PrivilegeId") REFERENCES "public"."pm_privilege" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_user_privilege" ADD CONSTRAINT "fk_up_ud" FOREIGN KEY ("UserId") REFERENCES "public"."pm_user" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_relation_user_role
-- ----------------------------
ALTER TABLE "public"."pm_relation_user_role" ADD CONSTRAINT "fk_rur_rd" FOREIGN KEY ("RoleId") REFERENCES "public"."pm_role" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT;
ALTER TABLE "public"."pm_relation_user_role" ADD CONSTRAINT "fk_rur_ud" FOREIGN KEY ("UserId") REFERENCES "public"."pm_user" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_role
-- ----------------------------
ALTER TABLE "public"."pm_role" ADD CONSTRAINT "fk_rsy" FOREIGN KEY ("SystemId") REFERENCES "public"."pm_system" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table pm_user
-- ----------------------------
ALTER TABLE "public"."pm_user" ADD CONSTRAINT "fk_us_sys" FOREIGN KEY ("SystemId") REFERENCES "public"."pm_system" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
