// formUtils.ts
import { UseFormGetValues, UseFormSetValue } from "react-hook-form";

export const handleCheckboxChange = <T>(
  event: React.ChangeEvent<HTMLInputElement>,
  fieldName: keyof T,
  setValue: UseFormSetValue<T>,
  getValues: UseFormGetValues<T>,
) => {
  const { value, checked } = event.target;
  const currentValues = (getValues(fieldName) as number[]) || [];

  if (checked) {
    setValue(fieldName, [...currentValues, parseInt(value)], {
      shouldValidate: true,
    });
  } else {
    setValue(
      fieldName,
      currentValues.filter((val) => val !== parseInt(value)),
      {
        shouldValidate: true,
      },
    );
  }
};
