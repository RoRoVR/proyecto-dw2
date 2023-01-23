import { useFormik } from "formik"
import { NavLink } from "react-router-dom";

export const RegisterPage = () => {
    const formulario = useFormik({
        initialValues: {
            nombre:'',
            apellido:'',
            descripcion:'des',
            username:'',
            password:'',
            password2:'',
        },
        onSubmit: async(v) => {
            const data = {
                id: 0,
                nombre:v.nombre,
                apellido:v.apellido,
                descripcion:'des',
                username:v.username,
                password:v.password,
                foto:'https://d500.epimg.net/cincodias/imagenes/2016/03/16/lifestyle/1458143779_942162_1458143814_noticia_normal.jpg',
            }
            console.log(data);

            try{
                const resultado = await fetch('http://localhost:5220/api/Usuario/Registar_Atualizar_Usuario', {
                    method: 'POST',
                    headers: {
                    'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });
    
                if(resultado.ok){
                    console.log('Se registro Correctamente');
                }
            }catch(e){
                console.log(e);
            }
            
        }
    });

  return (
    <div className="login-page">
            <form onSubmit={formulario.handleSubmit} className='login' >
                <h1 className='text-center mb-3' >Registrarse</h1>
                <div className="mb-3" >
                    <label htmlFor="nombre" className="form-label" >Nombre</label>
                    <input type="text" name="nombre" id="nombre" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.nombre}
                    />
                </div>

                <div className="mb-3" >
                    <label htmlFor="apellido" className="form-label" >Apellido</label>
                    <input type="text" name="apellido" id="apellido" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.apellido}
                    />
                </div>

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

                <div className="mb-3" >
                    <label htmlFor="password2" className="form-label" >Confirmar contraseña</label>
                    <input type="password" name="password2" id="password2" className="form-control"
                    onChange={formulario.handleChange}
                    value={formulario.values.password2}
                    />
                </div>

                <div>
                    <p>¿Ya tienes una cuenta? <NavLink className="text-primary" to='/login' > Inicia sesion aqui </NavLink></p>
                </div>

                <div className="d-flex justify-content-center " >
                    <button type="submit" className="btn btn-primary px-5 " >Registrarse</button>
                </div>

            </form>
        </div>
  )
}
