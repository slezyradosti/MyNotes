import { Form, Formik, Field } from "formik";
import { values } from "mobx";
import { Button, Input } from "semantic-ui-react";

function LoginForm() {
    return (
        <Formik
            initialValues={{ email: '', password: '' }}
            onSubmit={values => console.log(values)}
        >
            {({ handleSubmit, handleChange }) => (
                <Form
                    className='ui form'
                    onSubmit={handleSubmit}
                    autoComplete='off'
                >
                    <div className="ui form field">
                        <Input
                            required={true}
                            placeholder='Email'
                            name='email'
                            onChange={handleChange}
                        />
                    </div>
                    <div className="ui form field">
                        <Input
                            required={true}
                            placeholder='Password'
                            name='password'
                            type='password'
                            onChange={handleChange}
                        />
                    </div>
                    <Button positive content='Login' type='submit' fluid />
                </Form>
            )}

        </Formik>
    );
}

export default LoginForm;