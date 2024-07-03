namespace Laboratorium.ADO.Tables
{
    public static class Table
    {
        // commons
        public static readonly string ID = "id";
        public static readonly string LABO_ID = "labo_id";
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

        // table laboDataContrast
        public static readonly string POSITION = "position";
        public static readonly string IS_DELETED = "is_deleted";
        public static readonly string APPLICATOR_NAME = "applicator_name";
        public static readonly string SUBSTRATE = "substrate";
        public static readonly string CONTRAST = "contrast";
        public static readonly string TW = "tw";
        public static readonly string SP = "SP";
        public static readonly string COMMENTS = "comments";


        // LaboDataBasic table
        public static readonly string LABO_BASIC_DATA_TABLE = "LaboDataBasic";

        //LaboDataNrom
        public static readonly string LABO_NORM_TEST_TABLE = "LaboDataNorm";

        // LaboContrast table
        public static readonly string LABO_CONTRAST_TABLE = "LaboDataContrast";

        // LaboDataViscosity
        public static readonly string LABO_VISC_DATA_TABLE = "LaboDataViscosity";
        public static readonly string LABO_VISC_COL_DATA_TABLE = "LaboDataViscosityCol";

        // ContrastClass table
        public static readonly string CONTRAST_CLASS_TABLE = "CmbClassContrast";

        // GlossClass table
        public static readonly string GLOSS_CLASS_TABLE = "CmbClassGloss";

        // ScrubClass table
        public static readonly string SCRUB_CLASS_TABLE = "CmbClassScrub";

        // VocClass table
        public static readonly string VOC_CLASS_TABLE = "CmbClassVoc";

        // CmbNorm
        public static readonly string NORM_TABLE = "CmbNorm";

        // CmbNormDetail
        public static readonly string NORM_DETAIL_TABLE = "CmbNormdetail";

        // table Users
        public static readonly string USER_TABLE = "LaboUsers";

        // Material
        public static readonly string MATERIAL_TABLE = "Material";
        public static readonly string MATERIAL_H_CODE_TABLE = "MaterialClpCodeH";
        public static readonly string MATERIAL_P_CODE_TABLE = "MaterialClpCodeP";
        public static readonly string MATERIAL_HP_COMBINE_TABLE = "MaterialClpCodeH and MaterialClpCodeP";
        public static readonly string MATERIAL_GHS_CODE_TABLE = "MaterialClpCodeGHS";
        public static readonly string MATERIAL_SIGNAL_CODE_TABLE = "MaterialClpSignal";
        public static readonly string MATERIAL_COMPOSITION_TABLE = "MaterialComposition";
        public static readonly string COMPOUND_TABLE = "MaterialCompound";
        public static readonly string CMB_UNIT_TABLE = "CmbUnits";
        public static readonly string CMB_CURRENCY_TABLE = "CmbCurrency";
        public static readonly string CMB_MAT_FUNCTION_TABLE = "CmbMaterialFunction";

        // Clp data
        public static readonly string CLP_H_CODE_TABLE = "CmbClpHcode";
        public static readonly string CLP_P_CODE_TABLE = "CmbClpPcode";
        public static readonly string CLP_HP_CODE_TABLE = "CmbClpHcode and CmbClpPcode";
        public static readonly string CLP_SIGNAL_CODE_TABLE = "CmbClpSignalWord";
    }
}
