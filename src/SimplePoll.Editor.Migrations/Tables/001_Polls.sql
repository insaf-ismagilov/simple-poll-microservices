CREATE TABLE IF NOT EXISTS public.polls
(
    id                 int       NOT NULL GENERATED ALWAYS AS IDENTITY,
    title              text      NOT NULL,
    status             int       NOT NULL DEFAULT 0,
    type               int       NOT NULL DEFAULT 0,

    created_date       timestamp NOT NULL DEFAULT timezone('utc'::text, now()),
    last_modified_date timestamp NOT NULL DEFAULT timezone('utc'::text, now()),

    CONSTRAINT polls_pk PRIMARY KEY (id)
)