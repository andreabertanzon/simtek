create table if not exists customers
(
    id               serial
        primary key,
    name             varchar(255),
    surname          varchar(255),
    vat              varchar(255),
    email            varchar(255),
    stored           boolean   default false,
    creation_date    timestamp default CURRENT_TIMESTAMP,
    last_update_date timestamp default CURRENT_TIMESTAMP,
    address          varchar(500),
    phone_number     varchar(100)
);

alter table customers
    owner to andrea;

create table if not exists workers
(
    id               serial
        primary key,
    name             varchar(255),
    surname          varchar(255),
    pph              numeric(10, 2),
    stored           boolean   default false,
    creation_date    timestamp default CURRENT_TIMESTAMP,
    last_update_date timestamp default CURRENT_TIMESTAMP
);

alter table workers
    owner to andrea;

create table if not exists sites
(
    id               serial
        primary key,
    name             varchar(255)
        constraint unique_name
            unique,
    address          varchar(255),
    customer_id      integer
        references customers
            on delete cascade,
    stored           boolean   default false,
    creation_date    timestamp default CURRENT_TIMESTAMP,
    last_update_date timestamp default CURRENT_TIMESTAMP
);

alter table sites
    owner to andrea;

create table if not exists materials
(
    id               serial
        primary key,
    name             varchar(255),
    price            numeric(10, 2),
    unit             varchar(255),
    quantity         numeric(10, 2),
    stored           boolean   default false,
    creation_date    timestamp default CURRENT_TIMESTAMP,
    last_update_date timestamp default CURRENT_TIMESTAMP
);

alter table materials
    owner to andrea;

create table if not exists interventions
(
    id                serial
        primary key,
    site_id           integer
        references sites
            on delete cascade,
    intervention_date timestamp,
    stored            boolean   default false,
    creation_date     timestamp default CURRENT_TIMESTAMP,
    last_update_date  timestamp default CURRENT_TIMESTAMP,
    title             varchar(200),
    description       text
);

alter table interventions
    owner to andrea;

create table if not exists workerinterventions
(
    worker_id        integer not null
        references workers
            on delete cascade,
    intervention_id  integer not null
        references interventions
            on delete cascade,
    hours_worked     numeric(10, 2),
    stored           boolean   default false,
    creation_date    timestamp default CURRENT_TIMESTAMP,
    last_update_date timestamp default CURRENT_TIMESTAMP,
    primary key (worker_id, intervention_id)
);

alter table workerinterventions
    owner to andrea;

create table if not exists interventionmaterials
(
    intervention_id  integer
        references interventions
            on delete cascade,
    material_id      integer
        references materials
            on delete cascade,
    stored           boolean   default false,
    creation_date    timestamp default CURRENT_TIMESTAMP,
    last_update_date timestamp default CURRENT_TIMESTAMP,
    quantity         numeric
);

alter table interventionmaterials
    owner to andrea;

create or replace function mt_immutable_timestamp(value text) returns timestamp without time zone
    immutable
    language sql
as
$$
select value::timestamp

$$;

alter function mt_immutable_timestamp(text) owner to andrea;

create or replace function mt_immutable_timestamptz(value text) returns timestamp with time zone
    immutable
    language sql
as
$$
select value::timestamptz

$$;

alter function mt_immutable_timestamptz(text) owner to andrea;

create or replace function mt_grams_vector(text) returns tsvector
    immutable
    strict
    language plpgsql
as
$$
BEGIN
    RETURN (SELECT array_to_string(public.mt_grams_array($1), ' ') ::tsvector);
END
$$;

alter function mt_grams_vector(text) owner to andrea;

create or replace function mt_grams_query(text) returns tsquery
    immutable
    strict
    language plpgsql
as
$$
BEGIN
    RETURN (SELECT array_to_string(public.mt_grams_array($1), ' & ') ::tsquery);
END
$$;

alter function mt_grams_query(text) owner to andrea;

create or replace function mt_grams_array(words text) returns text[]
    immutable
    strict
    language plpgsql
as
$$
DECLARE
    result text[];
    DECLARE
    word text;
    DECLARE
    clean_word text;
BEGIN
    FOREACH
        word IN ARRAY string_to_array(words, ' ')
        LOOP
            clean_word = regexp_replace(word, '[^a-zA-Z0-9]+', '','g');
            FOR i IN 1 .. length(clean_word)
                LOOP
                    result := result || quote_literal(substr(lower(clean_word), i, 1));
                    result
                        := result || quote_literal(substr(lower(clean_word), i, 2));
                    result
                        := result || quote_literal(substr(lower(clean_word), i, 3));
                END LOOP;
        END LOOP;

    RETURN ARRAY(SELECT DISTINCT e FROM unnest(result) AS a(e) ORDER BY e);
END;
$$;

alter function mt_grams_array(text) owner to andrea;

create or replace function mt_upsert_intervention(doc jsonb, docdotnettype character varying, docid uuid, docversion uuid) returns uuid
    language plpgsql
as
$$
DECLARE
    final_version uuid;
BEGIN
    INSERT INTO public.mt_doc_intervention ("data", "mt_dotnet_type", "id", "mt_version", mt_last_modified) VALUES (doc, docDotNetType, docId, docVersion, transaction_timestamp())
    ON CONFLICT (id)
        DO UPDATE SET "data" = doc, "mt_dotnet_type" = docDotNetType, "mt_version" = docVersion, mt_last_modified = transaction_timestamp();

    SELECT mt_version FROM public.mt_doc_intervention into final_version WHERE id = docId ;
    RETURN final_version;
END;
$$;

alter function mt_upsert_intervention(jsonb, varchar, uuid, uuid) owner to andrea;

create or replace function mt_insert_intervention(doc jsonb, docdotnettype character varying, docid uuid, docversion uuid) returns uuid
    language plpgsql
as
$$
BEGIN
    INSERT INTO public.mt_doc_intervention ("data", "mt_dotnet_type", "id", "mt_version", mt_last_modified) VALUES (doc, docDotNetType, docId, docVersion, transaction_timestamp());

    RETURN docVersion;
END;
$$;

alter function mt_insert_intervention(jsonb, varchar, uuid, uuid) owner to andrea;

create or replace function mt_update_intervention(doc jsonb, docdotnettype character varying, docid uuid, docversion uuid) returns uuid
    language plpgsql
as
$$
DECLARE
    final_version uuid;
BEGIN
    UPDATE public.mt_doc_intervention SET "data" = doc, "mt_dotnet_type" = docDotNetType, "mt_version" = docVersion, mt_last_modified = transaction_timestamp() where id = docId;

    SELECT mt_version FROM public.mt_doc_intervention into final_version WHERE id = docId ;
    RETURN final_version;
END;
$$;

alter function mt_update_intervention(jsonb, varchar, uuid, uuid) owner to andrea;

