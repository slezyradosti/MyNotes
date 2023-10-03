import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { Button, Container, Header, Input, Label } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';
import ValidationErrors from "../errors/ValidationErrors";

function RegisterForm() {
    const { userStore } = useStore();

    return (
        <Container >
            <Formik
                initialValues={{ displayName: '', username: '', email: '', password: '', error: null }}
                onSubmit={(values, { setErrors }) => userStore.register(values).catch((error) => { console.log('catched error: ' + error); setErrors({ error }) }
                )}
                validationSchema={Yup.object({
                    displayName: Yup.string().required(),
                    username: Yup.string().required(),
                    email: Yup.string().required(),
                    password: Yup.string().required(),
                })}
            >
                {({ handleSubmit, handleChange, isSubmitting, errors, isValid, dirty }) => (
                    <Form
                        className='ui form error'
                        onSubmit={handleSubmit}
                        autoComplete='off'
                    >
                        <Header as='h2' content='Sign up to My Notes' color="teal" textAlign="center" />
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
                                placeholder='Display name'
                                name='displayName'
                                onChange={handleChange}
                            />
                        </div>
                        <div className="ui form field">
                            <Input
                                required={true}
                                placeholder='Username'
                                name='username'
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
                        <ErrorMessage name='error' render={() =>
                            <ValidationErrors errors={errors.error as unknown as string[]} />}
                        />
                        <Button
                            disabled={!isValid || !dirty || isSubmitting}
                            loading={isSubmitting}
                            positive content='Register'
                            type='submit' fluid
                        />
                    </Form>
                )}

            </Formik>
        </Container>
    );
}

export default observer(RegisterForm);