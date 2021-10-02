DROP FUNCTION IF EXISTS public.users_add;

CREATE OR REPLACE FUNCTION public.users_add(p_email text,
                                            p_password_hash text,
                                            p_first_name text,
                                            p_last_name text,
                                            p_user_role_id int)
    RETURNS int
    LANGUAGE sql
AS
$$
INSERT INTO public.users (role_id, email, password_hash, first_name, last_name)
VALUES (p_user_role_id, p_email, p_password_hash, p_first_name, p_last_name)
RETURNING id;
$$