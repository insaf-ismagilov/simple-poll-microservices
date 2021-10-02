DROP FUNCTION IF EXISTS public.users_update;

CREATE OR REPLACE FUNCTION public.users_update(p_id int,
                                               p_email text,
                                               p_password_hash text,
                                               p_first_name text,
                                               p_last_name text,
                                               p_user_role_id int)
    RETURNS int
    LANGUAGE sql
AS
$$
UPDATE public.users
SET email              = p_email,
    password_hash      = p_password_hash,
    first_name         = p_first_name,
    last_name          = p_last_name,
    role_id            = p_user_role_id,
    last_modified_date = now() at time zone 'utc'
WHERE id = p_id
RETURNING id;
$$