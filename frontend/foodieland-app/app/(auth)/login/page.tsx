'use client'

import {Logo, LoginForm, LinkButton} from '@/components'
export default function Login()
{
    return (
        <div className="flex-[0_1_100%] md:flex-[0_1_50%] flex flex-col justify-between">
            <div className="p-5">
                <Logo fontsize={24}/>
            </div>
            <div className="flex flex-col justify-between flex-grow gap-6">
                <div className="flex justify-center items-center h-full">
                    <div className="flex flex-col gap-5 items-start">
                        <h1>Log In</h1>
                        <LoginForm/>
                    </div>
                </div>
                <div className="flex items-center justify-between p-5">
                    <LinkButton url='/login' buttonText='Log in' />
                    <LinkButton url='/' buttonText='Skip' />
                </div>
            </div>
        </div>
    );
}