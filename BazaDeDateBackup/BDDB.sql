--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.0

-- Started on 2024-11-12 18:59:58

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

--
-- TOC entry 218 (class 1255 OID 25608)
-- Name: checkusercredentials(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.checkusercredentials(username character varying, password character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN EXISTS (
        SELECT 1
        FROM users
        WHERE users.username = CheckUserCredentials.username AND users.userpassword = CheckUserCredentials.password
    );
END;
$$;


ALTER FUNCTION public.checkusercredentials(username character varying, password character varying) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 25616)
-- Name: Users; Type: TABLE; Schema: public; Owner: Admin
--

CREATE TABLE public."Users" (
    id bigint NOT NULL,
    username text NOT NULL,
    userpassword text NOT NULL
);


ALTER TABLE public."Users" OWNER TO "Admin";

--
-- TOC entry 216 (class 1259 OID 25615)
-- Name: Users_id_seq; Type: SEQUENCE; Schema: public; Owner: Admin
--

ALTER TABLE public."Users" ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Users_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 215 (class 1259 OID 25610)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: Admin
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO "Admin";

--
-- TOC entry 4788 (class 0 OID 25616)
-- Dependencies: 217
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: Admin
--

COPY public."Users" (id, username, userpassword) FROM stdin;
1	test	test
5	Alex	asd
\.


--
-- TOC entry 4786 (class 0 OID 25610)
-- Dependencies: 215
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: Admin
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20241107121306_Init	8.0.10
\.


--
-- TOC entry 4794 (class 0 OID 0)
-- Dependencies: 216
-- Name: Users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: Admin
--

SELECT pg_catalog.setval('public."Users_id_seq"', 5, true);


--
-- TOC entry 4642 (class 2606 OID 25622)
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: Admin
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY (id);


--
-- TOC entry 4640 (class 2606 OID 25614)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: Admin
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


-- Completed on 2024-11-12 18:59:58

--
-- PostgreSQL database dump complete
--

