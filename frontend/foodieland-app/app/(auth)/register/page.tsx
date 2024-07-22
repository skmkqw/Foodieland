'use client'

import {Logo, RegisterForm, LinkButton} from '@/components'

export default function Register()
{
    return (
        <div className="flex-[0_1_100%] md:flex-[0_1_50%] flex flex-col justify-between">
            <div className="p-5">
                <Logo fontsize={24}/>
            </div>
            <div className="flex justify-center">
                <div className="flex flex-col gap-3 lg:gap-5 items-start">
                    <h1>Sign Up</h1>
                    <RegisterForm/>
                </div>
            </div>
            <div className="flex items-center justify-between p-5">
                <LinkButton url='/register' buttonText='Sign up' />
                <LinkButton url='/' buttonText='Skip' />
            </div>
        </div>
    );
}