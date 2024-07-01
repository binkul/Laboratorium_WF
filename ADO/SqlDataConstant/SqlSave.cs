using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public static class SqlSave
    {
        public static readonly Dictionary<SqlIndex, string> Save = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.CmbCurrencyIndex, "Insert Into Konkurencja.dbo.CmbCurrency(id, name, currency, rate) Values (@id, @name, @currency, @rate)" },
            {SqlIndex.CmbMaterialFunctionIndex, "Insert Into Konkurencja.dbo.CmbMaterialFunction(id, name_pl) Values (@id, @name_pl)" },
            {SqlIndex.LaboIndex, "Insert Into Konkurencja.dbo.DoswTytul(Numer_d, Data, Tytuł, Cel, UwagiWnioski, Observation, Gestosc, " +
                "IsDeleted, DateUpdated, UserId, ProjectId) Values(@Numer_d, @Data, @Tytul, @Cel, @UwagiWnioski, @Observation, @Gestosc, " +
                "@IsDeleted, @DateUpdated, @UserId, @ProjectId)" },
            {SqlIndex.LaboBasicIndex, "Insert Into Konkurencja.dbo.LaboDataBasic(labo_id, gloss_20, gloss_60, gloss_85, gloss_class, " +
                "gloss_comment, scrub_brush, scrub_sponge, scrub_class, scrub_comment, contrast_class, contrast_comment, voc, voc_class, " +
                "yield, yield_formula, adhesion, flow, spill, drying_I, drying_II, drying_III, drying_IV, drying_V, date_updated) " +
                "Output INSERTED.id Values(@labo_id, @gloss_20, @gloss_60, @gloss_85, @gloss_class, @gloss_comment, @scrub_brush, " +
                "@scrub_sponge, @scrub_class, @scrub_comment, @contrast_class, @contrast_comment, @voc, @voc_class, @yield, @yield_formula, " +
                "@adhesion, @flow, @spill, @drying_I, @drying_II, @drying_III, @drying_IV, @drying_V, @date_updated)" },
            {SqlIndex.LaboNormTestIndex, "Insert Into Konkurencja.dbo.LaboDataNorm(labo_id, position, norm, [description], requirement, result, substarte, " +
                "comment, date_created, date_updated, group_id) Output INSERTED.id Values(@labo_id, @position, @norm, @description, @requirement, @result, @substarte, " +
                "@comment, @date_created, @date_updated, @group_id)" },
            {SqlIndex.LaboViscosityIndex, "Insert Into Konkurencja.dbo.LaboDataViscosity(labo_id, to_compare, pH, temp, brook_1, brook_5, " +
                "brook_10, brook_20, brook_30, brook_40, brook_50, brook_60, brook_70, brook_80, brook_90, brook_100, brook_disc, " +
                "brook_comment, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, ici, ici_disc, ici_comment, date_created, " +
                "date_updated) Output INSERTED.id Values(@labo_id, @to_compare, @pH, @temp, @brook_1, @brook_5, @brook_10, @brook_20, " +
                "@brook_30, @brook_40, @brook_50, @brook_60, @brook_70, @brook_80, @brook_90, @brook_100, @brook_disc, @brook_comment, " +
                "@brook_x_vis, @brook_x_rpm, @brook_x_disc, @krebs, @krebs_comment, @ici, @ici_disc, @ici_comment, @date_created, @date_updated)" },
            {SqlIndex.LaboContrastIndex, "Insert Into Konkurencja.dbo.LaboDataContrast(labo_id, is_deleted, applicator_name, position, substrate, " +
                "contrast, tw, sp, comments, date_created, date_updated) Output INSERTED.id Values(@labo_id, @is_deleted, @applicator_name, @position, " +
                "@substrate, @contrast, @tw, @sp, @comments, @date_created, @date_updated)" },
            {SqlIndex.LaboViscosityColIndex, "Insert Into Konkurencja.dbo.LaboDataViscosityCol(labo_id, [type], [columns]) Values(@labo_id, @type, @columns)" },
            {SqlIndex.UserIndex, "Insert Into Konkurencja.dbo.LaboUsers(name, surname, e_mail, login, password, permission, identifier, active, date_created) " +
                 "Output INSERTED.id Values(@name, @surname, @e_mail, @login, @password, @permission, @identifier, @active, @date_created)"},
            {SqlIndex.MaterialIndex, "Insert Into Konkurencja.dbo.Material(name, index_db, supplier_id, function_id, is_intermediate, is_danger, is_production, is_observed, " +
                "is_active, is_package, price, price_per_quantity, price_transport, quantity, currency_id, unit_id, density, solids, ash_450, VOC, remarks, date_created, " +
                "date_updated) Output INSERTED.id Values(@name, @index_db, @supplier_id, @function_id, @is_intermediate, @is_danger, @is_production, @is_observed, @is_active, @is_package, @price, " +
                "@price_per_quantity, @price_transport, @quantity, @currency_id, @unit_id, @density, @solids, @ash_450, @VOC, @remarks, @date_created, @date_updated)" },
            {SqlIndex.MaterialClpSignalIndex, "Insert Into Konkurencja.dbo.MaterialClpSignal(material_id, code_id, date_created) Values(@material_id, @code_id, @date_created)" },
            {SqlIndex.MaterialClpGhsIndex, "Insert Into Konkurencja.dbo.MaterialClpCodeGHS(material_id, code_id, date_created) Values(@material_id, @code_id, @date_created)" },
            {SqlIndex.MaterialClpHcodeIndex, "Insert Into Konkurencja.dbo.MaterialClpCodeH(material_id, code_id, comments, date_created) Values (@material_id, @code_id, @comments, @date_created)" },
            {SqlIndex.MaterialClpPcodeIndex, "Insert Into Konkurencja.dbo.MaterialClpCodeP(material_id, code_id, comments, date_created) Values (@material_id, @code_id, @comments, @date_created)" },
            {SqlIndex.MaterialCompositionIndex, "Insert Into Konkurencja.dbo.MaterialComposition(material_id, compound_id, amount_min, amount_max, ordering, remarks, date_created) " +
                "Values(@material_id, @compound_id, @amount_min, @amount_max, @ordering, @remarks, @date_created)" },
        };
    }
}
