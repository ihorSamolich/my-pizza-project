import { VariantProps, cva } from "class-variance-authority";
import { classNames } from "utils/ClassNames.ts";

import React from "react";

const inputVariants = cva(
  "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 dark:shadow-sm-light",
  {
    variants: {
      variant: {
        default: "p-2.5 text-sm",
        outline: "bg-white border border-gray-300",
        filled: "bg-gray-100 border-none",
        file: "hidden",
      },
      size: {
        default: "p-2.5 text-sm",
        small: "p-1.5 text-xs",
        large: "p-3.5 text-lg",
      },
    },
    defaultVariants: {
      variant: "default",
      size: "default",
    },
  },
);

export interface InputProps
  extends Omit<React.InputHTMLAttributes<HTMLInputElement>, "size">,
    VariantProps<typeof inputVariants> {}
const Input = React.forwardRef<HTMLInputElement, InputProps>(({ className, size, variant, type, ...props }, ref) => {
  return <input type={type} className={classNames(inputVariants({ variant, size }), className)} ref={ref} {...props} />;
});

export default Input;
