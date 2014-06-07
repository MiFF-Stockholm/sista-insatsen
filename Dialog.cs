using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Dialog
{
    //Dialoger
    //Kör dialoger med DialogGUI.StartDialog från lämplig trigger se DialogTriggerStart och DialogNPCTrigger för exempel
    //hello_bye - debug
    //
    //intro - Första anropet från Tyrannus
    //fladder - När spelaren hittar Lord Fladder
    //fladder_efter - När Fladder är besegrad
    //
    //slum_fladder_död - När man kommer till slummen, om Fladder är död, kolla på Globals.FladderKilled
    //slum_fladder_lever - När man kommer till slummen, om Fladder är arresterad, kolla på Globals.FladderArested
    //rand - När spelaren hittar Mäster Rand
    //rand_efter - När Rand är besegrad
    //
    //templet - När spelaren kommer till Templet
    //präst - karaktärerna pratar med prästen
    //fjäll - när spelaren möter von fjäll
    //fjäll_efter - när von fjäll är besegrad
    //
    //intro_akt4 - utanför templet efter att von fjäll är död
    //luckan - när man hittar luckan
    //foxer - när man träffar Foxer
    //foxer_2 - när man har dödat Foxers livvakter
    //foxer_3 - när man har besegrat Foxer första gången
    //foxer_efter - när man har besegrat Foxer andra (och sista) gången

    static Dictionary<string, Dialog> dialogs = new Dictionary<string, Dialog>();
    static bool started = false;
    static void init()
    {
        if (started)
            return;

        started = true;

        //Hello Bye, debug dialog
        Dialog d = new Dialog();
        d.Text = "Hello!";
        DialogPath dp = new DialogPath();
        dp.Text = "Bye";
        dp.NextDialog = null;
        d.Options.Add(dp);
        d.Options.Add(dp);
        d.Options.Add(dp);
        dialogs.Add("hello_bye", d);

        //Akt1
        //Intro, anrop från Tyrannus
        d = new Dialog();
        d.Text = "<b>Tyrannus:</b> <i>Soldater, ni har handplockats bland de bästa av de bästa att nå dit ingen kan nå, göra det ingen kan göra. Ni kommer vara mina ögon och öron i en stad som gått åt helvetet. Ert första uppdrag är att lokalisera och neutralisera en \"Lord Fladder\"...</i>";
        d.FaceIndex = 2;
        dp = new DialogPath();
        Dialog d2 = new Dialog();
        d2.Text = "<b>Tyrannus:</b> <i>Han är en prominent figur på den svarta marknaden som sålt vapen till rebellerna i åratal. Våra agenter har jagat honom länge och vi har äntligen spårat honom till en lagerlokal åt väster. Lycka till, Tyrannus över.</i>";
        d2.FaceIndex = 2;
        dp.NextDialog = d2;
        d.Options.Add(dp);
        DialogPath dp2 = new DialogPath();
        d2.Options.Add(dp2);
        Dialog d3 = new Dialog();
        d3.Text = "<b>Pingvin:</b> Jag har hört om honom, extremt smart och väldigt farlig.";
        d3.FaceIndex = 1;
        dp2.NextDialog = d3;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d2 = new Dialog();
        d2.Text = "<b>Lejon:</b> Det gör det samma, order är order.";
        d2.FaceIndex = 4;
        dp.NextDialog = d2;
        dp2 = new DialogPath();
        d2.Options.Add(dp2);
        dp2.Text = "Framåt!";
        dialogs.Add("intro", d);

        //Första området i varuhuset, vid lord fladder
        d = new Dialog();
        d.Text = "<b>Fladder:</b> Så... ni har slutligen hittat mig. Jag har följt ert klumpiga försök till infiltration och är inte imponerad. Jag är nästan förolämpad av att de skickar sådana som er efter mig.";
        d.FaceIndex = 6;
        dp = new DialogPath();
        d2 = new Dialog();
        d2.Text = "<b>Ladybug:</b> Ge upp nu och ingen mer kommer till skada.";
        d2.FaceIndex = 3;
        dp.NextDialog = d2;
        d.Options.Add(dp);
        dp2 = new DialogPath();
        d2.Options.Add(dp2);
        d3 = new Dialog();
        d3.Text = "<b>Lejon:</b> Tystnad! Vi kom inte hit för att lyssna på ditt blabbel.";
        d3.FaceIndex = 4;
        dp2.NextDialog = d3;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d2 = new Dialog();
        d2.Text = "<b>Zebra:</b> Tyrannus vill se dig död, och det ska han få";
        d2.FaceIndex = 5;
        dp.NextDialog = d2;
        dp2 = new DialogPath();
        d2.Options.Add(dp2);
        d3 = new Dialog();
        d3.Text = "<b>Fladder:</b> Kom an då! Jag böjer mig aldrig för sånna som er!";
        d3.FaceIndex = 6;
        dp2.NextDialog = d3;        
        dp = new DialogPath();
        d3.Options.Add(dp);
        dp.Text = "Anfall!";
        dp.OnSelect = "CombatFladder";
        dialogs.Add("fladder", d);

        //Efter Fladder
        d = new Dialog();
        d.Text = "<b>Fladder:</b> Inget mer, jag ger mig! Snälla döda mig inte!";
        d.FaceIndex = 6;
        dp = new DialogPath();
        d2 = new Dialog();
        d2.Text = "<b>Zebra:</b> Nu är vår chans! Avsluta det...";
        d2.FaceIndex = 5;
        dp.NextDialog = d2;
        d.Options.Add(dp);
        dp2 = new DialogPath();
        d2.Options.Add(dp2);
        d3 = new Dialog();
        d3.Text = "<b>Pingvin:</b> Nej, vi kan tillfångata honom. Han är mer användbar levande.";
        d3.FaceIndex = 1;
        dp2.NextDialog = d3;
        dp = new DialogPath();
        dp.Text = "1. Arrestera honom!";
        d3.Options.Add(dp);
        Dialog d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Lejon:</b> I vingbojor är han ofarlig.";
        d4.FaceIndex = 4;
        DialogPath dp3 = new DialogPath();
        dp3.OnSelect = "FladderArested";
        d4.Options.Add(dp3);
        Dialog d5 = new Dialog();
        d5.Text = "<b>Fladder:</b> Nej, låt dem inte tortera mig!";
        d5.FaceIndex = 6;
        dp3.NextDialog = d5;
        dp3 = new DialogPath();
        d5.Options.Add(dp3);
        dp2 = new DialogPath();
        dp2.Text = "2. Döda honom!";
        d3.Options.Add(dp2);        
        d4 = new Dialog();
        dp2.NextDialog = d4;
        d4.Text = "<b>Lejon:</b> Order är order.";
        d4.FaceIndex = 4;
        dp3 = new DialogPath();
        dp3.OnSelect = "FladderKilled";
        d4.Options.Add(dp3);
        d5 = new Dialog();
        d5.Text = "<b>Fladder:</b> Neeeejjj! Gurgel...";
        d5.FaceIndex = 6;
        dp3.NextDialog = d5;
        dp3 = new DialogPath();
        d5.Options.Add(dp3);
        dialogs.Add("fladder_efter", d);

        //Akt 2
        //party utanför ingången till  slummen
        d = new Dialog();
        d.Text = "<b>Tyrranus:</b> <i>Bra jobbat. Detta kommer avsevärt försvåra för rebellerna i framtiden och Lord Fladder kommer inte störa oss igen.</i>";
        d.FaceIndex = 2;
        d2 = new Dialog();
        d2.Text = "<b>Tyrranus:</b> <i>Bra jobbat. Mina agenter har säkrat Fladder och för honom till en säker anläggning medans vi talar. Jag är övertygad att han kommer ge oss ovärdelig information.</i>";
        d2.FaceIndex = 2;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2.Options.Add(dp);
        d3 = new Dialog();
        d3.Text = "<b>Tyrannus:</b> <i>Ert nästa mål är en av rebellernas träningsanläggningar i förklädnad av ett katthem, direkt norr om er position. Ta er in och ta ut Mäster Rand som ansvarar för de nyrekryterade. Lycka till. Tyrannus över.</i>";
        d3.FaceIndex = 2;
        dp.NextDialog = d3;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Nyckelpigan:</b> Ska vi storma ett katthem? Civila skadade kommer bli oacceptabla!";
        d4.FaceIndex = 3;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Zebran:</b> Vem bryr sig om kattungar...";
        d5.FaceIndex = 5;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Lejonet:</b> Deras blod är på rebellernas händer som valde att gömma sig där i första taget.";
        d3.FaceIndex = 4;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Pingvinen:</b> …";
        d4.FaceIndex = 1;
        dp = new DialogPath();
        dp.Text = "Framåt!";
        d4.Options.Add(dp);
        dialogs.Add("slum_fladder_död", d);
        dialogs.Add("slum_fladder_lever", d2);

        //första träffandet med Mäster rand
        d = new Dialog();
        d.Text = "<b>Rand:</b> Har ni ingen skam kvar i era förtvinade själar. Komma stormandes med era automatvapen och sjuta allt som rör sig. Regimen har vänt sig till de värsta av de värsta för att utföra dens skitjobb. Jag gråter i själen vid när jag ser på hur lågt ni sjunkit.";
        d.FaceIndex = 9;
        dp = new DialogPath();
        d.Options.Add(dp);        
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Zebran:</b> Jaja, snacka kan alla. Om du kan slåss är jag mer intereserad av.";
        d2.FaceIndex = 5;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Lejonet:</b> Era rebelliska aktiviteter här avslutas nu.";
        d3.FaceIndex = 4;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Pingvinen:</b> Låt oss bara avsluta det här.";
        d4.FaceIndex = 1;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Nyckelpigan:</b> Det här är fel.";
        d5.FaceIndex = 3;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Rand:</b> Kom an! Grrrrr!";
        d2.FaceIndex = 9;
        dp = new DialogPath();
        dp.Text = "Anfall!!";
        dp.OnSelect = "CombatRand";
        d2.Options.Add(dp);
        dialogs.Add("rand", d);

        //-Rand besegrad-
        d = new Dialog();
        d.Text = "<b>Zebran:</b> Inte så tuff trots allt, vilken besvikelse...";
        d.FaceIndex = 5;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Rand:</b> Han vilseleder er och förtrycker alla! Hur ser ni inte det? Tyrannus är ingen annat än onskan själv som bara tänker på sig själv.";
        d2.FaceIndex = 9;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Lejonet:</b> Tyst! Du är inte in nån position att förolämpa vår rättmäkiga ledare, den store Tyrannus!";
        d3.FaceIndex = 4;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Rand:</b> Du är blind som en fladdermus! Det är sådana som du som inte kan tänka själva som är anledningen att monster som Tyrannus sitter kvar vid makten!";
        d4.FaceIndex = 9;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Lejon:</b> <color=red>TYSTNAD!</color> Jag ska visa dig vad ett monster verkligen innebär...";
        d5.FaceIndex = 4;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Rand:</b> Mjaaaarghhh!\n(död)";
        d2.FaceIndex = 9;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Pingvinen:</b> Titta en karta, verkar föreställa templet i den norra delen av staden. Låt oss undersöka det.";
        d3.FaceIndex = 1;
        dp = new DialogPath();
        d3.Options.Add(dp);
        dp.OnSelect = "DropMap";
        dp.Text = "Mot templet!";
        dialogs.Add("rand_efter", d);

        //Akt 3
        //-utanför templet-
        d = new Dialog();
        d.Text = "<b>Tyrannus:</b> <i>Utmärkt, Rand var en högt uppsatt rebellöverste och han kommer vara svårersatt. Men denna karta ororar mig. Jag vill att ni undersöker templet utförligt och ta hand om potentialla hot.</i>";
        d.FaceIndex = 2;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Lejonet:</b> Uppfattat, omdelbart ers höghet.";
        d2.FaceIndex = 4;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Tyrannus:</b> <i>Tyrannus över.</i>";
        d3.FaceIndex = 2;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Pingvinen:</b> Var tror ni kartan leder, och vad betyder nummren?";
        d4.FaceIndex = 1;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Zebran:</b> Den tillhörde Mäster Rand så det är antagligen nått rebelltillhåll.";
        d5.FaceIndex = 5;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Nyckelpigan:</b> Prästen kanske vet nått.";
        d2.FaceIndex = 3;
        dp = new DialogPath();
        dp.Text = "Framåt!";
        d2.Options.Add(dp);
        dialogs.Add("templet", d);

        //-när de taler med prästen-
        d = new Dialog();
        d.Text = "<b>Prästen:</b> Ja, mina lamm?";
        d.FaceIndex = 10;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Zebran:</b> Förklara det här! Den här kartan fanns i besittning av en högt uppsatt rebellofficer!";
        d2.FaceIndex = 5;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Prästen:</b> Jag vet inte vad du talar om.";
        d3.FaceIndex = 10;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Lejonet:</b> Ljug inte för oss. Alla vet vad som händer dem som ljuger för regeringstjänstemän.";
        d4.FaceIndex = 4;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Prästen:</b> Snälla, jag är ingen rebell!";
        d5.FaceIndex = 10;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Zebran:</b> Han ljuger, jag kan se det i hans ögon.";
        d2.FaceIndex = 5;
        dp2 = new DialogPath();
        dp2.Text = "1. Lämna prästen";
        d2.Options.Add(dp2);
        dp = new DialogPath();
        dp.Text = "2. Hota prästen";
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Zebran:</b> Vi är utskickade av Tyrannus själv, vi kan göra vad vi vill så länge det hjälper uppdraget... Har du någonsin blivit toterad förut? Första gången är alltid den bästa.";
        d3.FaceIndex = 5;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Pingvinen:</b> Jag skulle berätta vad du vet om jag var du. Han menar allvar.";
        d4.FaceIndex = 1;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Prästen:</b> Snälla! Jag har familj!";
        d5.FaceIndex = 10;
        d5.Options.Add(dp2);
        dp = new DialogPath();
        dp.Text = "2. Bryt några fingrar";
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Prästen:</b> <color=red>AAAAAAAGHRRRAAAARGHARAGAAAA!</color>";
        d2.FaceIndex = 10;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Nyckelpigan:</b> Är det här verkilgen nödvändigt? Jag menar vi borde väl kunna lista ut det själva.";
        d3.FaceIndex = 3;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Lejonet:</b> Om han vet så är det här snabbare. Uppdraget är det viktigaste.";
        d4.FaceIndex = 4;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Zebran:</b> Snart är han bruten.";
        d5.FaceIndex = 5;
        d5.Options.Add(dp2);
        dp = new DialogPath();
        dp.Text = "2. Bryt resten";
        d5.Options.Add(dp);        
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Prästen:</b> OK! OK! Jag berättar vad jag vet, bara sluta snälla!";
        d2.FaceIndex = 10;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Zebran:</b> Så ska det låta.";
        d3.FaceIndex = 5;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Prästen:</b> Statyn längst fram till höger öppnar den hemliga gången! Det är allt jag vet, snälla tro mig...\n(Prästen svimmar)";
        d4.FaceIndex = 10;
        dp = new DialogPath();
        dp.Text = "Till Pelaren";
        dp.OnSelect = "PriestOut";
        d4.Options.Add(dp);
        dialogs.Add("präst", d);

        //-när de hittar boss 3, Von Fjäll-
        d = new Dialog();
        d.Text = "<b>Zebran:</b> Där är han! Det där är Von Fjäll, överstelöjtnant i rebellarméen!";
        d.FaceIndex = 5;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();        
        dp.NextDialog = d2;
        d2.Text = "<b>Fjäll:</b> Dö regeringsskum! Raaaargh!";
        d2.FaceIndex = 7;
        dp = new DialogPath();
        dp.Text = "Anfall!!";
        dp.OnSelect = "CombatFjall";
        d2.Options.Add(dp);
        dialogs.Add("fjäll", d);

        //-Fjäll dör-
        d = new Dialog();
        d.Text = "<b>Fjäll:</b> Råååååår, jag har misslyckats!\n(dör)";
        d.FaceIndex = 7;
        dp = new DialogPath();
        dp.Text = "Bra jobbat!";
        d.Options.Add(dp);
        dialogs.Add("fjäll_efter", d);

        //Akt 4
        //-utanför templet-
        d = new Dialog();
        d.Text = "<b>Tyrannus:</b> <i>Hmm, det är oroande att de hade ett gömme så mitt i stan som vi inte kände till men det är gott att Von Fjäll är omändertagen. Bra jobbat åter igen, nu återstår bara ett mål, Överbefälhavade Foxer.</i>";
        d.FaceIndex = 2;
        dp = new DialogPath();
        d.Options.Add(dp);        
        Dialog d6 = new Dialog();
        dp.NextDialog = d6;
        d6.Text = "<b>Tyrannus:</b> <i>Han har undvikit oss i månader men vi vet att han är i stan då vi spårade ett medelande från Von Fjäll när ni tog ut honom riktat mot västra delen av industriområdet. Jag vill att ni berer er dit och lokaliserar målet, det finns en god chanse att det är rebellernas huvudbas och att det är där Foxer gömmer sig. Ni valdes för det här, framtidens stabilitet ligger i era händer att utföra detta sista uppdrag.</i>";
        dp = new DialogPath();
        d6.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Lejonet:</b> Uppfattat, jag förstår.";
        d2.FaceIndex = 4;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Tyrannus:</b> <i>Gott, lycka till. Tyrannus över.</i>";
        d3.FaceIndex = 2;;
        dp = new DialogPath();
        d3.Options.Add(dp);
        Dialog d7 = new Dialog();
        dp.NextDialog = d7;
        d7.Text = "<b>Zebran:</b> Äntligen! Ett uppdrag värdigt av min skicklighet. Det här är vår chans att bli ihågkommna föralltid som hjältar.";
        d7.FaceIndex = 5;
        dp = new DialogPath();
        d7.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Nyckelpigan:</b> Eller som kallblodiga mördare...";
        d4.FaceIndex = 3;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Lejonet:</b> Det är inte ert jobb att fundera över följderna, vi är soldater. Soldater lyder order.";
        d5.FaceIndex = 4;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Pingvinen:</b> Men om det nu faktiskt är rebellernas bas, ska vi fyra själva storma den? Varför inte skicka en armé?";
        d2.FaceIndex = 1;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Zebran:</b> Då kommer de få en chans att fly igen, Von Fjäll har redan varnat dem. Ironiskt nog så skulle vi nog aldrig hitta dem om det inte vore för hans varning, om det nu var till basen han skickade den.";
        d3.FaceIndex = 5;
        dp = new DialogPath();
        dp.Text = "Framåt!";
        d3.Options.Add(dp);
        dialogs.Add("intro_akt4", d);

        //-en lucka i marken har poppat upp i västra industriområdet som leder till Rebellbasen-
        d = new Dialog();
        d.Text = "<b>Pingvinen:</b> Vad konstigt, den här har jag aldrig sett.";
        d.FaceIndex = 1;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Nycklepigan:</b> Kan ha varit holoflerad. Gammal magi, effektiv men opåliglig.";
        d2.FaceIndex = 3;
        dp = new DialogPath();
        d2.Options.Add(dp);        
        dialogs.Add("luckan", d);

        //-när de hittar Foxer-
        d = new Dialog();
        d.Text = "<b>Lejonet:</b> Överbeälhavare Foxer!";
        d.FaceIndex = 4;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Foxer:</b> Så Tyrannus knähundar har nosat upp mig trots allt.";
        d2.FaceIndex = 8;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Pingvinen:</b> Allt tack vare din trogne dumbom Von Fjäll.";
        d3.FaceIndex = 1;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Foxer:</b> Von Fjäll levde och dog för det han trodde på, våga inte håna hans minne!";
        d4.FaceIndex = 8;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Zebran:</b> Haha, döda Zooborger är tysta Zooborger... Du kan inte fly den här gången Foxer.";
        d5.FaceIndex = 5;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Foxer:</b> Fly? Nej, jag tänker inte fly. Jag har varit på flyckten alldeles för länge. Ni har mördat mina vänner kors och tvärs, det är tid att någon stoppar er.";
        d2.FaceIndex = 8;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Zebran:</b> Jag har hört tillräckligt!";
        d3.FaceIndex = 5;
        dp = new DialogPath();
        d3.Options.Add(dp);
        //d4 = new Dialog();
        //dp.NextDialog = d4;
        //d4.Text = "<b>Livvakter:</b> Vi låter er inte skada Överbefälhavare Foxer!";
        //dp = new DialogPath();
        dp.Text = "Anfall!!!";
        dp.OnSelect = "CombatFoxer";
        //d4.Options.Add(dp);
        dialogs.Add("foxer", d);

        //-Strid med livvakter-
        d = new Dialog();
        d.Text = "<b>Foxer:</b> Tror ni verkligen att döda mig kommer lösa era problem? Det är inte jag som är problemet, jag är bara en reaktion på det verkliga problemet och om ni dödar mig kommer bara någon annan ta min plats. ";
        d.FaceIndex = 8;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Nyckelpigan:</b> Ni dödar civila, röjer och bränner! Det finns ingen heder i det!";
        d2.FaceIndex = 3;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Foxer:</b> Lögner! Lögner som Tyrannus spridit genom att låta sina egna trupper härja! Vem tror ni han skyller alla de döda kattungarna ni lämnade efter er på katthemmet? Oss! Ser ni inte att det är  Tyrannus som är det verkliga problemet?";
        d3.FaceIndex = 8;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Lejonet:</b> Låt honom inte lura er, vi har våra order och om vi inte kan lita på dem vad har vi då att tro på?";
        d4.FaceIndex = 4;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Foxer:</b> Du är så fast i ditt dogmatiska tänkande att du inte ens hör hur löjlig du låter själv.";
        d5.FaceIndex = 8;
        dp = new DialogPath();
        d5.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Lejonet:</b> Raaargh!";
        d2.FaceIndex = 4;
        dp = new DialogPath();
        dp.Text = "Döda honom!";
        d2.Options.Add(dp);        
        dialogs.Add("foxer_2", d);

        //-första bossetapen, blir snack efter (han är inte död än)-
        d = new Dialog();
        d.Text = "<b>Foxer:</b> Vi skulle inte behöva slåss! Med er styrka och mitt ledarskap så kan störta Tyrannus en gång för alla! Det skulle gagna alla och det vet ni innerst inne, det ser jag på er.";
        d.FaceIndex = 8;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Zebran:</b> Försök inte. Tror du verkligen att vi skulle falla för sådana billiga trix!";
        d2.FaceIndex = 5;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Foxer:</b> Det är inget trix. Jag säger bara sanningen.";
        d3.FaceIndex = 8;
        dp = new DialogPath();
        d3.Options.Add(dp);
        d4 = new Dialog();
        dp.NextDialog = d4;
        d4.Text = "<b>Nyckelpigan:</b> Jag blev inte militär för sån här skit.";
        d4.FaceIndex = 3;
        dp = new DialogPath();
        d4.Options.Add(dp);
        d5 = new Dialog();
        dp.NextDialog = d5;
        d5.Text = "<b>Lejonet:</b> <color=red>DÖÖÖÖÖÖ!</color>";
        dp = new DialogPath();
        d5.Options.Add(dp);
        dp.Text = "På honom igen!";
        dialogs.Add("foxer_3", d);

        //-nu dör faktiskt Foxer-
        d = new Dialog();
        d.Text = "<b>Foxer:</b> Nån annan kommer ta min plats... Revolutionen kommer aldrig att dö! Ahhh...";
        d.FaceIndex = 8;
        dp = new DialogPath();
        d.Options.Add(dp);
        d2 = new Dialog();
        dp.NextDialog = d2;
        d2.Text = "<b>Tyrannus:</b> <i>Utmärkt jobbat som alltid. Nu kanske vi kan få lite fred när upprorsmakaren Foxer är död.</i>";
        d2.FaceIndex = 2;
        dp = new DialogPath();
        d2.Options.Add(dp);
        d3 = new Dialog();
        dp.NextDialog = d3;
        d3.Text = "<b>Nyckelpigan:</b> Jag hoppas det var värt det.";
        d3.FaceIndex = 3;
        dp = new DialogPath();
        dp.Text = "Bra Jobbat...";
        dp.OnSelect = "RunCredits";
        d3.Options.Add(dp);        
        dialogs.Add("foxer_efter", d);
    }
    public static Dialog GetDialog(string name)
    {
        if (!dialogs.ContainsKey(name))
        {
            if (started)
                return null;

            init();
            return GetDialog(name);
        }
        return dialogs[name];
    }
    public static List<string> PlayedDialogs = new List<string>();

    public string Text = "Hello!";
    public int FaceIndex = 0;
    public List<DialogPath> Options = new List<DialogPath>();
}

public class DialogPath
{
    public string Text = "Fortsätt...";
    public string OnSelect = null;
    public Dialog NextDialog = null;
}
