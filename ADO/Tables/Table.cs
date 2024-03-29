﻿namespace Laboratorium.ADO.Tables
{
    public static class Table
    {
        // commons
        public static readonly string ID = "id";
        public static readonly string DATE_CREATED = "date_created";
        public static readonly string DATE_UPDATED = "date_updated";
        public static readonly string NAME_PL = "name_pl";
        public static readonly string NAME_EN = "name_en";


        // Main Labo table
        public static readonly string LABO_TABLE = "DoswTytul";
        public static readonly string LABO_NUMBER_D = "Numer_d";
        public static readonly string LABO_DATE_CREATED = "Data";
        public static readonly string LABO_PROJECT_ID = "Cykl";
        public static readonly string LABO_TITLE = "Tytuł";
        public static readonly string LABO_GOAL = "Cel";
        public static readonly string LABO_COCLUSION = "UwagiWnioski";
        public static readonly string LABO_DENSITY = "Gestosc";
        public static readonly string LABO_OBSERVATION = "Observation";
        public static readonly string LABO_IS_DELETED = "IsDeleted";

        // LabDataBasic table
        public static readonly string LABO_BASIC_DATA_TABLE = "LaboDataBasic";

        // ContrastClass table
        public static readonly string CONTRAST_CLASS_TABLE = "CmbClassContrast";

        // GlossClass table
        public static readonly string GLOSS_CLASS_TABLE = "CmbClassGloss";

        // ScrubClass table
        public static readonly string SCRUB_CLASS_TABLE = "CmbClassScrub";

        // VocClass table
        public static readonly string VOC_CLASS_TABLE = "CmbClassVoc";

        // table Users
        public static readonly string USER_TABLE = "LaboUsers";

        // table project
        public static readonly string PROJECT_TABLE = "Project";
        public static readonly string PROJECT_NAME = "name";
        public static readonly string PROJECT_COMMENTS = "comments";
        public static readonly string PROJECT_ARCHIVE = "is_archive";
        public static readonly string PROJECT_LABO = "is_labo";
        public static readonly string PROJECT_AUCTION = "is_auction";
        public static readonly string PROJECT_DISC = "local_disc";
        public static readonly string PROJECT_DATE = "date";
        public static readonly string PROJECT_USER = "ovner";

        // table projectSubCategory
        public static readonly string PROJECT_SUB_TABLE = "ProjectSubCategory";
        public static readonly string PROJECT_ID = "project_id";
    }
}
