import { useFormik } from "formik";
import { NavLink, useNavigate } from "react-router-dom";

function LoginPage({setUsername}) {
    const navigate = useNavigate();


    const formulario = useFormik({
        initialValues: {
            username:'',
            password:''
        },
        onSubmit: async(v) => {

            try {
                const response = await fetch(`http://localhost:5220/api/Usuario/Usuario_Login?user=${v.username}&pass=${v.password}`)
                if (response.ok) {
                    const data = await response.json();
                    sessionStorage.setItem('data', JSON.stringify(data));
                    window.location.reload(false);
                }
            } catch (error) {
                console.log(error)
            }



        }
    });

    return ( 
        <div className="login-page">
            <form onSubmit={formulario.handleSubmit} className='login' >
                <h1 className="text-center mb-3" >Iniciar sesion</h1>
                <div className="mb-3" >
                    <label htmlFor="username" className="form-label" >Usuario</label>
                    <input type="text" name="username" id="username" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.username}
                    />
                </div>

                <div className="mb-3" >
                    <label htmlFor="password" className="form-label" >Contraseña</label>
                    <input type="password" name="password" id="password" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.password}
                    />
                </div>

                <div>
                    <p>¿No tienes una cuenta? <NavLink className="text-primary" to='/register' > Registrate aqui </NavLink></p>
                </div>

                <div className="d-flex justify-content-center " >
                    <button type="submit" className="btn btn-primary px-5 " >Iniciar</button>
                </div>

            </form>
        </div>
     );
}
export default LoginPage;