CREATE TABLE IF NOT EXISTS public.user_roles
(
    id                 int                         NOT NULL,
    name               varchar                     NOT NULL,
    created_date       timestamp NOT NULL DEFAULT timezone('utc'::text, now()),
    last_modified_date timestamp NOT NULL DEFAULT timezone('utc'::text, now()),

    CONSTRAINT user_roles_pk PRIMARY KEY (id)
)