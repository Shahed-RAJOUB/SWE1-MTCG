--
-- PostgreSQL database dump
--

-- Dumped from database version 12.3
-- Dumped by pg_dump version 12.3

-- Started on 2021-01-09 19:50:40

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 204 (class 1259 OID 17698)
-- Name: cards; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.cards (
    "card-id" text NOT NULL,
    name text,
    element text,
    type text,
    damage numeric,
    "deckId" integer,
    "package-id" integer
);


ALTER TABLE public.cards OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 17742)
-- Name: packages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.packages (
    "package-id" integer,
    "card-id" text
);


ALTER TABLE public.packages OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 17722)
-- Name: score; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.score (
    "stat-id" integer,
    score integer DEFAULT 100,
    "session-id" integer
);


ALTER TABLE public.score OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 17685)
-- Name: session; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.session (
    "firstId" integer,
    "secondId" integer DEFAULT 0,
    "sessionId" integer NOT NULL
);


ALTER TABLE public.session OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 17825)
-- Name: session_sesssion-id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."session_sesssion-id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."session_sesssion-id_seq" OWNER TO postgres;

--
-- TOC entry 2882 (class 0 OID 0)
-- Dependencies: 214
-- Name: session_sesssion-id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."session_sesssion-id_seq" OWNED BY public.session."sessionId";


--
-- TOC entry 205 (class 1259 OID 17714)
-- Name: stack; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.stack (
    collection text,
    "package-id" integer,
    "stack-id" integer
);


ALTER TABLE public.stack OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 17760)
-- Name: trading; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.trading (
    "card-id" text,
    "user-id" integer,
    "trade-id" integer NOT NULL
);


ALTER TABLE public.trading OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 17758)
-- Name: trading_trade-id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."trading_trade-id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."trading_trade-id_seq" OWNER TO postgres;

--
-- TOC entry 2883 (class 0 OID 0)
-- Dependencies: 208
-- Name: trading_trade-id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."trading_trade-id_seq" OWNED BY public.trading."trade-id";


--
-- TOC entry 202 (class 1259 OID 17677)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    password text,
    username text,
    "user-id" integer NOT NULL,
    "stack-id" integer NOT NULL,
    "stat-id" integer NOT NULL,
    "deck-id" integer NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 17815)
-- Name: users_deck-id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."users_deck-id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."users_deck-id_seq" OWNER TO postgres;

--
-- TOC entry 2884 (class 0 OID 0)
-- Dependencies: 213
-- Name: users_deck-id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."users_deck-id_seq" OWNED BY public.users."deck-id";


--
-- TOC entry 211 (class 1259 OID 17797)
-- Name: users_stack-id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."users_stack-id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."users_stack-id_seq" OWNER TO postgres;

--
-- TOC entry 2885 (class 0 OID 0)
-- Dependencies: 211
-- Name: users_stack-id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."users_stack-id_seq" OWNED BY public.users."stack-id";


--
-- TOC entry 212 (class 1259 OID 17806)
-- Name: users_stat-id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."users_stat-id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."users_stat-id_seq" OWNER TO postgres;

--
-- TOC entry 2886 (class 0 OID 0)
-- Dependencies: 212
-- Name: users_stat-id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."users_stat-id_seq" OWNED BY public.users."stat-id";


--
-- TOC entry 210 (class 1259 OID 17788)
-- Name: users_user-id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."users_user-id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."users_user-id_seq" OWNER TO postgres;

--
-- TOC entry 2887 (class 0 OID 0)
-- Dependencies: 210
-- Name: users_user-id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."users_user-id_seq" OWNED BY public.users."user-id";


--
-- TOC entry 2730 (class 2604 OID 17827)
-- Name: session sessionId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.session ALTER COLUMN "sessionId" SET DEFAULT nextval('public."session_sesssion-id_seq"'::regclass);


--
-- TOC entry 2733 (class 2604 OID 17763)
-- Name: trading trade-id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trading ALTER COLUMN "trade-id" SET DEFAULT nextval('public."trading_trade-id_seq"'::regclass);


--
-- TOC entry 2726 (class 2604 OID 17790)
-- Name: users user-id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN "user-id" SET DEFAULT nextval('public."users_user-id_seq"'::regclass);


--
-- TOC entry 2727 (class 2604 OID 17799)
-- Name: users stack-id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN "stack-id" SET DEFAULT nextval('public."users_stack-id_seq"'::regclass);


--
-- TOC entry 2728 (class 2604 OID 17808)
-- Name: users stat-id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN "stat-id" SET DEFAULT nextval('public."users_stat-id_seq"'::regclass);


--
-- TOC entry 2729 (class 2604 OID 17817)
-- Name: users deck-id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN "deck-id" SET DEFAULT nextval('public."users_deck-id_seq"'::regclass);


--
-- TOC entry 2866 (class 0 OID 17698)
-- Dependencies: 204
-- Data for Name: cards; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.cards ("card-id", name, element, type, damage, "deckId", "package-id") FROM stdin;
1	WaterGoblin	Water	Monster	10.2	\N	\N
2	FireTroll	Fire	Monster	12.0	\N	\N
3	FireSpell	Fire	Spell	15.0	\N	\N
8	FireDragon	Fire	Monster	25.5	\N	\N
9	Elve	normal	Monster	15.5	\N	\N
10	trap	normal	Spell	15.0	2	2
15	shark	water	monster	10	1	3
11	Zombie	normal	monster	10	2	3
12	firetrap	fire	spell	10	2	3
13	fish	water	monster	10	2	3
14	phinex	fire	monster	10	2	3
4	WaterSpell	Water	Spell	10.0	3	\N
5	RegularSpell	normal	Spell	20.0	3	\N
6	Knight	normal	Monster	25.0	3	\N
7	WaterDragon	Water	Monster	30.5	3	\N
\.


--
-- TOC entry 2869 (class 0 OID 17742)
-- Dependencies: 207
-- Data for Name: packages; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.packages ("package-id", "card-id") FROM stdin;
\N	\N
\.


--
-- TOC entry 2868 (class 0 OID 17722)
-- Dependencies: 206
-- Data for Name: score; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.score ("stat-id", score, "session-id") FROM stdin;
5	20	2
2	73	35
3	66	35
\.


--
-- TOC entry 2865 (class 0 OID 17685)
-- Dependencies: 203
-- Data for Name: session; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.session ("firstId", "secondId", "sessionId") FROM stdin;
2	3	35
3	2	2
2	3	32
2	3	33
2	3	34
\.


--
-- TOC entry 2867 (class 0 OID 17714)
-- Dependencies: 205
-- Data for Name: stack; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.stack (collection, "package-id", "stack-id") FROM stdin;
second	2	2
first	1	2
0	0	2
new package	2	2
new package	3	2
new package	2	3
\.


--
-- TOC entry 2871 (class 0 OID 17760)
-- Dependencies: 209
-- Data for Name: trading; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.trading ("card-id", "user-id", "trade-id") FROM stdin;
14	2	5
11	2	6
11	2	7
11	3	8
11	2	1
\.


--
-- TOC entry 2864 (class 0 OID 17677)
-- Dependencies: 202
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (password, username, "user-id", "stack-id", "stat-id", "deck-id") FROM stdin;
istrator	admin	1	1	1	1
password	shahd	2	2	2	2
pass	Tom	3	3	3	3
pass	Sara	4	4	4	4
pass	Tom	5	5	5	5
pass	stefan	6	6	6	6
pass	stefan	7	7	7	7
is	fadi	8	8	8	8
sara	Sara	9	9	9	9
sara	Sara	10	10	10	10
sara	Sara	11	11	11	11
sara	Sara	12	12	12	12
\.


--
-- TOC entry 2888 (class 0 OID 0)
-- Dependencies: 214
-- Name: session_sesssion-id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."session_sesssion-id_seq"', 35, true);


--
-- TOC entry 2889 (class 0 OID 0)
-- Dependencies: 208
-- Name: trading_trade-id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."trading_trade-id_seq"', 8, true);


--
-- TOC entry 2890 (class 0 OID 0)
-- Dependencies: 213
-- Name: users_deck-id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."users_deck-id_seq"', 12, true);


--
-- TOC entry 2891 (class 0 OID 0)
-- Dependencies: 211
-- Name: users_stack-id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."users_stack-id_seq"', 12, true);


--
-- TOC entry 2892 (class 0 OID 0)
-- Dependencies: 212
-- Name: users_stat-id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."users_stat-id_seq"', 12, true);


--
-- TOC entry 2893 (class 0 OID 0)
-- Dependencies: 210
-- Name: users_user-id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."users_user-id_seq"', 12, true);


--
-- TOC entry 2735 (class 2606 OID 17705)
-- Name: cards cards_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cards
    ADD CONSTRAINT cards_pkey PRIMARY KEY ("card-id");


--
-- TOC entry 2737 (class 2606 OID 17768)
-- Name: trading trading_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trading
    ADD CONSTRAINT trading_pkey PRIMARY KEY ("trade-id");


-- Completed on 2021-01-09 19:50:40

--
-- PostgreSQL database dump complete
--

