DROP FUNCTION IF EXISTS public.users_get_by_email;

CREATE OR REPLACE FUNCTION public.users_get_by_email(
    p_email text
)
    RETURNS table
            (
                id                      int,
                role_id                 int,
                role_name               text,
                role_created_date       timestamp,
                role_last_modified_date timestamp,
                email                   text,
                password_hash           text,
                first_name              text,
                last_name               text,
                created_date            timestamp,
                last_modified_date      timestamp
            )
    LANGUAGE sql
AS
$$
SELECT u.id,
       ur.id                 AS role_id,
       ur.name               AS role_name,
       ur.created_date       AS role_created_date,
       ur.last_modified_date AS role_last_modified_date,
       u.email,
       u.password_hash,
       u.first_name,
       u.last_name,
       u.created_date,
       u.last_modified_date
FROM public.users u
         INNER JOIN public.user_roles ur on ur.id = u.role_id
WHERE u.email = p_email;
$$;