import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { Button, Container, Header, Input, Label } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";

function LoginForm() {
    const { userStore } = useStore();

    return (
        <Container >
            <Formik
                initialValues={{ email: '', password: '', error: null }}
                onSubmit={(values, { setErrors }) => userStore.login(values).catch(() =>
                    setErrors({ error: 'Invalid email or password' }))}
            >
                {({ handleSubmit, handleChange, isSubmitting, errors }) => (
                    <Form
                        className='ui form'
                        onSubmit={handleSubmit}
                        autoComplete='off'
                    >
                        <Header as='h2' content='Login to MyNotes' color="grey" textAlign="center" />
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
                        <ErrorMessage name='error' render={() =>
                            <Label style={{ marginBottom: 10 }} basic color='red' content={errors.error}
                            />}
                        />
                        <Button
                            loading={isSubmitting}
                            content='Login'
                            type='submit'
                            fluid
                            className="submitBtnColor Border" />
                    </Form>
                )}

            </Formik>
        </Container>
    );
}

export default observer(LoginForm);