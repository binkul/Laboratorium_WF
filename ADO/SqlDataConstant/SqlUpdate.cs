﻿using System.Collections.Generic;

namespace Laboratorium.ADO.SqlDataConstant
{
    public abstract class SqlUpdate
    {
        public static readonly Dictionary<SqlIndex, string> Update = new Dictionary<SqlIndex, string>
        {
            {SqlIndex.LaboIndex, "Update Konkurencja.dbo.DoswTytul Set Tytuł=@Tytul, Cel=@Cel, UwagiWnioski=@UwagiWnioski, Gestosc=@Gestosc, " +
                "Wyrob=@Wyrob, WyrEksperyment=@WyrEksperyment, WyrNazwa=@WyrNazwa, WyrSkrot=@WyrSkrot, WyrFunkcja1=@WyrFunkcja1, " +
                "WyrFunkcja2=@WyrFunkcja2, WyrPolysk=@WyrPolysk, WyrArchiwum=@WyrArchiwum, WyrNiebezpieczny=@WyrNiebezpieczny, WyrIndex=@WyrIndex, " +
                "IsDeleted=@IsDeleted, DateUpdated=@DateUpdated, ProjectId=@ProjectId Where Numer_d=@Numer_d" },
            {SqlIndex.LaboBasicIndex, "Update Konkurencja.dbo.LaboDataBasic Set gloss_20=@gloss_20, gloss_60=@gloss_60, gloss_85=@gloss_85, " +
                "gloss_class=@gloss_class, gloss_comment=@gloss_comment, scrub_brush=@scrub_brush, scrub_sponge=@scrub_sponge, scrub_class=@scrub_class, " +
                "scrub_comment=@scrub_comment, contrast_class=@contrast_class, contrast_comment=@contrast_comment, voc=@voc, voc_class=@voc_class, " +
                "yield=@yield, yield_formula=@yield_formula, adhesion=@adhesion, flow=@flow, spill=@spill, drying_I=@drying_I, drying_II=@drying_II, " +
                "drying_III=@drying_III, drying_IV=@drying_IV, drying_V=@drying_V, date_updated=@date_updated Where labo_id=@labo_id" },
            {SqlIndex.LaboViscosityIndex, "Update Konkurencja.dbo.LaboDataViscosity Set to_compare=@to_compare, pH=@pH, temp=@temp, brook_1=@brook_1, brook_5=@brook_5, " +
                "brook_10=@brook_10, brook_20=@brook_20, brook_30=@brook_30, brook_40=@brook_40, brook_50=@brook_50, brook_60=@brook_60, brook_70=@brook_70, brook_80=@brook_80, brook_90=@brook_90, brook_100=@brook_100, brook_disc=@brook_disc, " +
                "brook_comment=@brook_comment, brook_x_vis=@brook_x_vis, brook_x_rpm=@brook_x_rpm, brook_x_disc=@brook_x_disc, krebs=@krebs, krebs_comment=@krebs_comment, ici=@ici, ici_disc=@ici_disc, ici_comment=@ici_comment, date_created=@date_created, " +
                "date_updated=@date_updated Where id=@id" },
            {SqlIndex.UserIndex, "Update Konkurencja.dbo.LaboUsers Set name=@name, surname=@surname, e_mail=@e_mail, [login]=@login, [password]=@password, permission=@permission, " +
                "identifier=@identifier, active=@active, date_created=@date_created) Where id=@id" }
        };
    }
}
