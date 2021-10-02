CREATE TABLE IF NOT EXISTS public.users
(
    id                 int                         NOT NULL GENERATED ALWAYS AS IDENTITY,
    role_id            int                         NOT NULL UNIQUE,
    email              text                        NOT NULL,
    password_hash      text,
    first_name         text,
    last_name          text,
    created_date       timestamp NOT NULL DEFAULT timezone('utc'::text, now()),
    last_modified_date timestamp NOT NULL DEFAULT timezone('utc'::text, now()),

    CONSTRAINT users_pk PRIMARY KEY (id),
    CONSTRAINT users_user_roles_fk FOREIGN KEY (role_id) REFERENCES public.user_roles (id)
)