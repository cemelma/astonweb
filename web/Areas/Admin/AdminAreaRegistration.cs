using System.Web.Mvc;

namespace web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("loginpagex", "yonetim/login_eski", new { action = "Login", Controller = "Account" });
            context.MapRoute("loginpagex_v2", "yonetim/login", new { action = "Login_v2", Controller = "Account" });
            context.MapRoute("logoutx", "cikis", new { action = "Logout", Controller = "Account" });
            context.MapRoute("homepage_defaultx", "yonetim", new { action = "Index", Controller = "Home" });
            context.MapRoute("homepagex", "yonetim/anasayfa", new { action = "Index", Controller = "Home" });

            context.MapRoute("tags", "yonetim/sayfataglari/{id}/{lang}", new { action = "Index", Controller = "Tags" });
            context.MapRoute("tags2", "yonetim/sayfataglari/{id}", new { action = "Index", Controller = "Tags" });
            //context.MapRoute("homepage_prm", "yonetim/{type}", new { action = "Index", Controller = "Home" });
            //context.MapRoute("homepage_all", "yonetim/teklif/tumteklifler", new { action = "AllList", Controller = "Home" });
            //context.MapRoute("homepage_detail", "yonetim/teklifdetay/{id}", new { action = "Details", Controller = "Home" });
            context.MapRoute("instituional_misionx", "yonetim/kurumsal/misyon", new { action = "Misyon", Controller = "Institutional" });
            context.MapRoute("instituional_visionx", "yonetim/kurumsal/vizyon", new { action = "Vizyon", Controller = "Institutional" });
            context.MapRoute("instituional_hakkimizdax", "yonetim/kurumsal/hakkimizda", new { action = "Hakkimizda", Controller = "Institutional" });
            context.MapRoute("instituional_nedenbizx", "yonetim/kurumsal/nedenbiz", new { action = "WhyUs", Controller = "Institutional" });

            //context.MapRoute("instituional_ekibimizx", "yonetim/kurumsal/ekibimiz", new { action = "Ekibimiz", Controller = "OurTeam" });
            context.MapRoute("instituional_danismanlarimizx", "yonetim/kurumsal/danismanlarimiz", new { action = "Danismanlarimiz", Controller = "OurTeam" });
            context.MapRoute("instituional_uzmanlikalanlarimizx", "yonetim/kurumsal/uzmanlikalanlarimiz", new { action = "UzmanlikAlanlarimiz", Controller = "OurTeam" });

            context.MapRoute("instituional_misionx2", "yonetim/kurumsal/misyon/{lang}", new { action = "Misyon", Controller = "Institutional" });
            context.MapRoute("instituional_visionx2", "yonetim/kurumsal/vizyon/{lang}", new { action = "Vizyon", Controller = "Institutional" });
            context.MapRoute("instituional_hakkimizdax2", "yonetim/kurumsal/hakkimizda/{lang}", new { action = "Hakkimizda", Controller = "Institutional" });
            context.MapRoute("instituional_nedenbizx2", "yonetim/kurumsal/nedenbiz/{lang}", new { action = "WhyUs", Controller = "Institutional" });

            //context.MapRoute("instituional_ekibimiz2", "yonetim/kurumsal/ekibimiz/{lang}", new { action = "Ekibimiz", Controller = "Institutional" });
            context.MapRoute("instituional_danismanlarimiz2", "yonetim/kurumsal/danismanlarimiz/{lang}", new { action = "Danismanlarimiz", Controller = "OurTeam" });
            context.MapRoute("instituional_uzmanlikalanlarimiz2", "yonetim/kurumsal/uzmanlikalanlarimiz/{lang}", new { action = "UzmanlikAlanlarimiz", Controller = "OurTeam" });

            context.MapRoute("instituional_yenisayfa", "yonetim/kurumsal/yeni", new { action = "Create", Controller = "Institutional" });
            context.MapRoute("instituional_indexx", "yonetim/kurumsal", new { action = "Index", Controller = "Institutional" });
            context.MapRoute("instituional_indexx2", "yonetim/kurumsal/{lang}", new { action = "Index", Controller = "Institutional" });
            context.MapRoute("instituional_editsayfa", "yonetim/kurumsal/edit/{id}", new { action = "Create", Controller = "Institutional" });
            


            context.MapRoute("ourservices_indexx", "yonetim/hizmetlerimiz", new { action = "OurServices", Controller = "Service" });
            context.MapRoute("ourservices_indexx1", "yonetim/hizmetlerimiz/{lang}", new { action = "OurServices", Controller = "Service" });

            //HABERLER
            context.MapRoute("news_defaultx", "yonetim/haberler", new { action = "Index", Controller = "News" }, null, new[] { "web.Areas.Admin.Controllers" });
            context.MapRoute("newsx", "yonetim/haberler/{lang}", new { action = "Index", Controller = "News" });
            context.MapRoute("newsaddx", "yonetim/haberekle", new { action = "AddNews", Controller = "News" });
            context.MapRoute("newseditx", "yonetim/haberduzenle/{id}", new { action = "EditNews", Controller = "News" });

            //EKİBİMİZ
            context.MapRoute("ekibimiz_defaultx", "yonetim/ekibimiz", new { action = "Index", Controller = "OurTeam" }, null, new[] { "web.Areas.Admin.Controllers" });
            context.MapRoute("ekibimizx", "yonetim/ekibimiz/{lang}", new { action = "Index", Controller = "OurTeam" });
            context.MapRoute("ekibimizaddx", "yonetim/ekibimizekle", new { action = "AddOurTeam", Controller = "OurTeam" });
            context.MapRoute("ekibimizeditx", "yonetim/ekibimizduzenle/{id}", new { action = "EditOurTeam", Controller = "OurTeam" });

            //PROJECTS
            context.MapRoute("service_defaultx", "yonetim/hizmetler", new { action = "Index", Controller = "Service" });
            context.MapRoute("servicex", "yonetim/hizmetler/{lang}", new { action = "Index", Controller = "Service" });
            context.MapRoute("servicex2", "yonetim/hizmetler/{lang}/{id}", new { action = "Index", Controller = "Service" });
            context.MapRoute("serviceaddx", "yonetim/hizmetekle", new { action = "AddService", Controller = "Service" });
            context.MapRoute("serviceeditx", "yonetim/hizmetduzenle/{id}", new { action = "EditService", Controller = "Service" });

            context.MapRoute("servicegroup_defaultx", "yonetim/hizmetgruplari", new { action = "Index", Controller = "ServiceGroup" });
            context.MapRoute("servicegroupx", "yonetim/hizmetgruplari/{lang}", new { action = "Index", Controller = "ServiceGroup" });
            context.MapRoute("servicegroupaddx", "yonetim/hizmetgrubuekle", new { action = "AddServiceGroup", Controller = "ServiceGroup" });
            context.MapRoute("servicegroupeditx", "yonetim/hizmetgrubuduzenle/{id}", new { action = "EditServiceGroup", Controller = "ServiceGroup" });

            //SECTORS

            context.MapRoute("semtekle", "yonetim/semtekle", new { action = "AddDistrict", Controller = "Location" });
            context.MapRoute("semtduzenle", "yonetim/semtduzenle/{id}", new { action = "EditDistrict", Controller = "Location" });
            context.MapRoute("semtliste", "yonetim/semtliste", new { action = "DistrictList", Controller = "Location" });

            context.MapRoute("ilceekle", "yonetim/ilceekle", new { action = "AddCity", Controller = "Location" });
            context.MapRoute("ilceduzenle", "yonetim/ilceduzenle/{id}", new { action = "EditCity", Controller = "Location" });
            context.MapRoute("ilceliste", "yonetim/ilceliste", new { action = "CityList", Controller = "Location" });

            context.MapRoute("ilekle", "yonetim/ilekle", new { action = "AddCountry", Controller = "Location" });
            context.MapRoute("ilduzenle", "yonetim/ilduzenle/{id}", new { action = "EditCountry", Controller = "Location" });
            context.MapRoute("illiste", "yonetim/illiste", new { action = "CountryList", Controller = "Location" });

            context.MapRoute("sector_defaultx", "yonetim/sektorler", new { action = "Index", Controller = "Sector" });
            context.MapRoute("sectorx", "yonetim/sektorler/{lang}", new { action = "Index", Controller = "Sector" });
            context.MapRoute("sectorx2", "yonetim/sektorler/{lang}/{id}", new { action = "Index", Controller = "Sector" });
            context.MapRoute("sectoraddx", "yonetim/sektorekle", new { action = "AddSector", Controller = "Sector" });
            context.MapRoute("sectoreditx", "yonetim/sektorduzenle/{id}", new { action = "EditSector", Controller = "Sector" });

            context.MapRoute("sectorgroup_defaultx", "yonetim/sektorgruplari", new { action = "Index", Controller = "SectorGroup" });
            context.MapRoute("sectorgroupx", "yonetim/sektorgruplari/{lang}", new { action = "Index", Controller = "SectorGroup" });
            context.MapRoute("sectorgroupaddx", "yonetim/sektorgrubuekle", new { action = "AddSectorGroup", Controller = "SectorGroup" });
            context.MapRoute("sectorgroupeditx", "yonetim/sektorgrubuduzenle/{id}", new { action = "EditSectorGroup", Controller = "SectorGroup" });

            context.MapRoute("oursectors_indexx", "yonetim/sektorlerimiz", new { action = "OurSectors", Controller = "Sector" });
            context.MapRoute("oursectors_indexx1", "yonetim/sektorlerimiz/{lang}", new { action = "OurSectors", Controller = "Sector" });

            //PROJECTS
            context.MapRoute("project_defaultx", "yonetim/projeler", new { action = "Index", Controller = "Project" });
            context.MapRoute("projectx", "yonetim/projeler/{lang}", new { action = "Index", Controller = "Project" });
            context.MapRoute("projectaddx", "yonetim/projeekle", new { action = "AddProject", Controller = "Project" });
            context.MapRoute("projecteditx", "yonetim/projeduzenle/{id}", new { action = "EditProject", Controller = "Project" });

            context.MapRoute("projectgroups_defaultx", "yonetim/projegruplari", new { action = "Index", Controller = "ProjectGroup" });
            context.MapRoute("projectgroupsx", "yonetim/projegruplari/{lang}", new { action = "Index", Controller = "ProjectGroup" });

            //PROJECT_REFERENCES
            context.MapRoute("projectreference_defaultx", "yonetim/projereferanslari", new { action = "Index", Controller = "ProjectReference" });
            context.MapRoute("projectreferencex", "yonetim/projereferanslari/{lang}", new { action = "Index", Controller = "ProjectReference" });
            context.MapRoute("projectreferenceaddx", "yonetim/projereferansekle", new { action = "AddProjectReference", Controller = "ProjectReference" });
            context.MapRoute("projectreferenceeditx", "yonetim/projereferansduzenle/{id}", new { action = "EditProjectReference", Controller = "ProjectReference" });

            context.MapRoute("projectreferencegroups_defaultx", "yonetim/projereferansgruplari", new { action = "Index", Controller = "ProjectReferenceGroup" });
            context.MapRoute("projectreferencegroupsx", "yonetim/projereferansgruplari/{lang}", new { action = "Index", Controller = "ProjectReferenceGroup" });
            context.MapRoute("projectreferencex2", "yonetim/projereferanslari/{lang}/{id}", new { action = "Index", Controller = "ProjectReference" });
            //TEKLİFLER
         //   context.MapRoute("teklif_default", "yonetim/tumteklifler", new { action = "Index", Controller = "Teklif" });
            context.MapRoute("teklifx", "yonetim/teklifler/{type}", new { action = "Index", Controller = "Teklif" });
            context.MapRoute("teklif_detailx", "yonetim/teklifler/detay/{id}", new { action = "Details", Controller = "Teklif" });
            context.MapRoute("teklif_silx", "yonetim/teklifler/sil/{id}/{type}", new { action = "Delete", Controller = "Teklif" });
           
            //LİnKLER
            context.MapRoute("link_defaultx", "yonetim/linkler", new { action = "Index", Controller = "Link" });
            context.MapRoute("linkaddx", "yonetim/linkekle", new { action = "AddLink", Controller = "Link" });
            context.MapRoute("linkeditx", "yonetim/linkduzenle/{id}", new { action = "EditLink", Controller = "Link" });
            context.MapRoute("linksx", "yonetim/linkler/{lang}", new { action = "Index", Controller = "Link" });

            //Ekipmanlar
            context.MapRoute("equipment_defaultx", "yonetim/ekipmanlar", new { action = "Index", Controller = "Equipment" });
            context.MapRoute("equipmentaddx", "yonetim/ekipmanekle", new { action = "Add", Controller = "Equipment" });
            context.MapRoute("equipmenteditx", "yonetim/ekipmanduzenle/{id}", new { action = "Edit", Controller = "Equipment" });
            context.MapRoute("equipmentx", "yonetim/ekipmanlar/{lang}", new { action = "Index", Controller = "Equipment" });


            //REFERANSLAR
            context.MapRoute("references_defaultx", "yonetim/referanslar", new { action = "Index", Controller = "Reference" });
            context.MapRoute("referencesx", "yonetim/referanslar/{lang}", new { action = "Index", Controller = "Reference" });
            context.MapRoute("referenceaddx", "yonetim/referansekle", new { action = "AddReference", Controller = "Reference" });
            context.MapRoute("referenceeditx", "yonetim/referansduzenle/{id}", new { action = "EditReference", Controller = "Reference" });

            //REFERANSLAR
            context.MapRoute("banner_defaultx", "yonetim/banner", new { action = "Index", Controller = "Banner" });
            context.MapRoute("bannerx", "yonetim/banner/{lang}", new { action = "Index", Controller = "Banner" });
            context.MapRoute("banneraddx", "yonetim/bannerekle", new { action = "Add", Controller = "Banner" });
            context.MapRoute("bannereditx", "yonetim/bannerduzenle/{id}", new { action = "Edit", Controller = "Banner" });

            //IS ORTAKLARI
            context.MapRoute("solutionpartner_defaultx", "yonetim/cozumortaklari", new { action = "Index", Controller = "SolutionPartner" });
            context.MapRoute("solutionpartnerx", "yonetim/cozumortaklari/{lang}", new { action = "Index", Controller = "SolutionPartner" });
            context.MapRoute("solutionpartneraddx", "yonetim/cozumortagiekle", new { action = "AddSolutionPartner", Controller = "SolutionPartner" });
            context.MapRoute("solutionpartnereditx", "yonetim/cozumortagiduzenle/{id}", new { action = "EditSolutionPartner", Controller = "SolutionPartner" });

            //IS ORTAKLARI
            context.MapRoute("certificate_defaultx", "yonetim/sertifikalar", new { action = "Index", Controller = "Certificate" });
            context.MapRoute("certificatex", "yonetim/sertifikalar/{lang}", new { action = "Index", Controller = "Certificate" });
            context.MapRoute("certificateaddx", "yonetim/sertifikaekle", new { action = "AddCertificate", Controller = "Certificate" });
            context.MapRoute("certificateeditx", "yonetim/sertifikaduzenle/{id}", new { action = "EditCertificate", Controller = "Certificate" });

            //MAİL KULLANICILARI
            context.MapRoute("mailuser_defx", "yonetim/mailkullanicilari", new { action = "Index", Controller = "Mail" });
            context.MapRoute("mailuserx", "yonetim/mailkullanicilari/{type}", new { action = "Index", Controller = "Mail" });
            context.MapRoute("mailuser_addx", "yonetim/ekle", new { action = "Add", Controller = "Mail" });
            context.MapRoute("mailuser_editx", "yonetim/duzenle/{id}", new { action = "Edit", Controller = "Mail" });
            context.MapRoute("mail_settingx", "yonetim/mailayarlari", new { action = "MailSetting", Controller = "Mail" });


            //DÖKÜMANLAR
            context.MapRoute("documentsx", "yonetim/dokumanlar", new { action = "Index", Controller = "Documents" });
            context.MapRoute("documentsgroups_defaultx", "yonetim/dokumangruplari", new { action = "Index", Controller = "DocumentGroup" });
            context.MapRoute("documentsgroupsx", "yonetim/dokumangruplari/{lang}", new { action = "Index", Controller = "DocumentGroup" });
            context.MapRoute("adddocumentx", "yonetim/dokumanekle", new { action = "AddDocument", Controller = "Documents" });
            context.MapRoute("editdocumentx", "yonetim/dokumanduzenle/{id}", new { action = "EditDocument", Controller = "Documents" });
            context.MapRoute("documentlist_defaultx", "yonetim/dokumanlistesi", new { action = "Index", Controller = "Documents" }, null, new[] { "web.Areas.Admin.Controllers" });
            context.MapRoute("documentlistx", "yonetim/dokumanlistesi/{lang}", new { action = "Index", Controller = "Documents" });
            context.MapRoute("documentlist_twoparamx", "yonetim/dokumanlistesi/{lang}/{id}", new { action = "Index", Controller = "Documents" });


            //ÜRÜNLER
            context.MapRoute("productsx", "yonetim/urunler", new { action = "Index", Controller = "Product" });
            
            context.MapRoute("productsgroups_defaultx", "yonetim/urungruplari", new { action = "Index", Controller = "ProductGroup" });
            context.MapRoute("productsgroups_editx", "yonetim/urungrubuduzenle/{id}", new { action = "EdtiGroup", Controller = "ProductGroup" });
            context.MapRoute("productsgroupsx", "yonetim/urungruplari/{lang}", new { action = "Index", Controller = "ProductGroup" });
            context.MapRoute("productsgroups_defaultx1", "yonetim/urungruplari/basliklar/{id}", new { action = "Baslik", Controller = "ProductGroup" });
            context.MapRoute("productssubgroups_defaultx", "yonetim/urunaltgruplari", new { action = "Index", Controller = "ProductSubGroup" });
            context.MapRoute("productssubgroups_editx", "yonetim/urunaltgrubuduzenle/{id}", new { action = "EdtiGroup", Controller = "ProductSubGroup" });
            context.MapRoute("productssubgroupsx", "yonetim/urunaltgruplari/{lang}", new { action = "Index", Controller = "ProductSubGroup" });
            context.MapRoute("productssubgroupsx2", "yonetim/urunaltgruplari/{lang}/{id}", new { action = "Index", Controller = "ProductSubGroup" });

            context.MapRoute("productsubbestadd2", "yonetim/urunaltgrubu/{id}/{subid}", new { action = "Add", Controller = "ProductSubSubbestGroup" });
            context.MapRoute("productsubbestedit2", "yonetim/urunaltgrubuedit/{id}/{subid}", new { action = "Edit", Controller = "ProductSubSubbestGroup" });
            
            context.MapRoute("productsubbestadd", "yonetim/urunaltgrubu/{id}", new { action = "Add", Controller = "ProductSubbestGroup" });
            context.MapRoute("productsubbestedit", "yonetim/urunaltgrubuedit/{id}", new { action = "Edit", Controller = "ProductSubbestGroup" });

            context.MapRoute("addproductsx", "yonetim/urunekle", new { action = "AddProduct", Controller = "Product" });
            context.MapRoute("addproductsxdd", "yonetim/urunekle/{id}", new { action = "AddProduct", Controller = "Product" });
            context.MapRoute("editproductsx", "yonetim/urunduzenle/{id}", new { action = "EditProduct", Controller = "Product" });
            context.MapRoute("productslist_defaultx", "yonetim/urunlistesi", new { action = "Index", Controller = "Product" }, null, new[] { "web.Areas.Admin.Controllers" });
            context.MapRoute("productslistx", "yonetim/urunlistesi/{lang}", new { action = "Index", Controller = "Product" });
            context.MapRoute("productslist_twoparamx", "yonetim/urunlistesi/{lang}/{id}", new { action = "Index", Controller = "Product" });

            //RESİM GALERİ
            context.MapRoute("gallerygroupx", "yonetim/galeriler", new { action = "Index", Controller = "Gallery" });
            context.MapRoute("gallerygroups_defaultx", "yonetim/galerigruplari", new { action = "Index", Controller = "GalleryGroup" });
            context.MapRoute("gallerygroupsx", "yonetim/galerigruplari/{lang}", new { action = "Index", Controller = "GalleryGroup" });
            context.MapRoute("galleryimageaddx", "yonetim/resimekle", new { action = "AddImage", Controller = "Gallery" });
            context.MapRoute("gallerylistx", "yonetim/galeriresimleri", new { action = "GalleryList", Controller = "Gallery" });


            //BANKA BİLGİLERİ
            context.MapRoute("bank_defaultx", "yonetim/bankabilgileri", new { action = "Index", Controller = "Bank" });
            context.MapRoute("bankx", "yonetim/bankabilgileri/{lang}", new { action = "Index", Controller = "Bank" });
            context.MapRoute("bankaddx", "yonetim/bankabilgisiekle", new { action = "AddBank", Controller = "Bank" });
            context.MapRoute("bankeditx", "yonetim/bankabilgisiduzenle/{id}", new { action = "EditBank", Controller = "Bank" });

            // İLETİŞİM

            context.MapRoute("contact_defaultx", "yonetim/iletisim", new { action = "Index", Controller = "Contact" });
            context.MapRoute("contact_defaultx1", "yonetim/iletisim-mesajlari", new { action = "Messages", Controller = "Contact" });
            context.MapRoute("contactx", "yonetim/iletisim/{lang}", new { action = "Index", Controller = "Contact" });

            // İNSAN KAYNAKLARI
            context.MapRoute("ik_positionsx", "yonetim/insankaynaklari/pozisyonlar", new { action = "HumanResourcePositions", Controller = "HumanResource" });
            context.MapRoute("ik_positionsx2", "yonetim/insankaynaklari/pozisyonlar/{lang}", new { action = "HumanResourcePositions", Controller = "HumanResource" });
            context.MapRoute("ik_positionaddx", "yonetim/insankaynaklari/pozisyonekle", new { action = "AddHumanResourcePosition", Controller = "HumanResource" });
            context.MapRoute("ik_positioneditx", "yonetim/insankaynaklari/pozisyonduzenle/{id}", new { action = "EditHumanResourcePosition", Controller = "HumanResource" });
            context.MapRoute("ik_indexx", "yonetim/insankaynaklari", new { action = "Index", Controller = "HumanResource" });
            context.MapRoute("ik_cv", "yonetim/insankaynaklari/cvListesi", new { action = "ListCV", Controller = "HumanResource" });
            context.MapRoute("ik_cvdetail", "yonetim/insankaynaklari/cvListesi/{id}", new { action = "ListCV", Controller = "HumanResource" });
            context.MapRoute("ik_index2", "yonetim/insankaynaklari/{lang}", new { action = "Index", Controller = "HumanResource" });

            //KULLANICILAR
            context.MapRoute("newuseradd", "yonetim/yenikullanici", new { action = "New", Controller = "Account" });
            context.MapRoute("userList", "yonetim/kullanicilar", new { action = "Index", Controller = "Account" });
            context.MapRoute("useredit", "yonetim/kullaniduzenle/{id}", new { action = "Edit", Controller = "Account" });


            //SOSYAL MEDYA LİNKLERİ
            context.MapRoute("socialmedia", "yonetim/sosyalmedyalinkleri", new { action = "Index", Controller = "SocialMedia" });
            context.MapRoute("socialmedia_add", "yonetim/yenilink", new { action = "Add", Controller = "SocialMedia" });
            context.MapRoute("socialmedia_edit", "yonetim/sosyalmedyalinkduzenle/{id}", new { action = "Edit", Controller = "SocialMedia" });
            context.MapRoute("socialmedia_editx", "yonetim/linkduzenle/{id}", new { action = "Edit", Controller = "SocialMedia" });
            //context.MapRoute("bannereditx", "yonetim/bannerduzenle/{id}", new { action = "Edit", Controller = "Banner" });



            context.MapRoute("town_defaultx", "yonetim/ilceler", new { action = "Index", Controller = "Town" });
            context.MapRoute("townx", "yonetim/ilceler/{id}", new { action = "Index", Controller = "Town" });
            context.MapRoute("townaddx", "yonetim/yeniilce", new { action = "Create", Controller = "Town" });
            context.MapRoute("towneditx", "yonetim/ilceyiduzenle/{id}", new { action = "Edit", Controller = "Town" });

            context.MapRoute("district_defaultx", "yonetim/semtler", new { action = "Index", Controller = "District" });
            context.MapRoute("districtx", "yonetim/semtler/{cid}/{tid}", new { action = "Index", Controller = "District" });
            context.MapRoute("districtx2", "yonetim/semtler/{cid}", new { action = "Index", Controller = "District" });
            context.MapRoute("districtaddx", "yonetim/yenisemt", new { action = "Create", Controller = "District" });
            context.MapRoute("districteditx", "yonetim/semtiduzenle/{id}", new { action = "Edit", Controller = "District" });


            //SOSYAL MEDYA LİNKLERİ
            context.MapRoute("estate_default", "yonetim/emlaklistesi", new { action = "Index", Controller = "Estate" });
            context.MapRoute("estate_lang", "yonetim/emlaklistesi/{lang}", new { action = "Index", Controller = "Estate" });

            context.MapRoute("estate_add", "yonetim/yeniemlak", new { action = "Add", Controller = "Estate" });
            context.MapRoute("estate_add1", "yonetim/yeniemlak/{lang}", new { action = "Add", Controller = "Estate" });
            context.MapRoute("estate_edit", "yonetim/emlakduzenle/{id}", new { action = "Edit", Controller = "Estate" });

           

            context.MapRoute("songuncellemeler", "yonetim/songuncellemeler", new { action = "Index", Controller = "Updates" });

            context.MapRoute("add_prodcategory_default", "yonetim/urun-kategorisi-ekle", new { action = "Add", Controller = "ProdCategory" });
            context.MapRoute("add_prodcategory", "yonetim/urun-kategorisi-ekle/{lang}", new { action = "Add", Controller = "ProdCategory" });

            context.MapRoute("analyticpage", "yonetim/analitic", new { action = "Index", Controller = "Analytic" });

            context.MapRoute("brifingss", "yonetim/brifingler", new { action = "Index", Controller = "Brifing" });
            context.MapRoute("brifingsdetails", "yonetim/detaylibrifingler", new { action = "Detail", Controller = "Brifing" });
            context.MapRoute("brifingsdetailsinfo", "yonetim/detaylibrifingler/detay/{id}", new { action = "DetailInfo", Controller = "Brifing" });

            context.MapRoute("uyeler", "yonetim/uyeler", new { action = "Index", Controller = "Users" });


            context.MapRoute(
                "Admin_defaultx",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
